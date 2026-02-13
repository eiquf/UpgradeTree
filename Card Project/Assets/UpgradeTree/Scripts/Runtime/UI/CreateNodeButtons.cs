//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNodeButtons : IElement<RectTransform>
{
    private readonly NodeTree _tree;
    private readonly GameObject _nodeUIPrefab;
    private readonly Dictionary<Node, GameObject> _spawnedNodes;
    private readonly INodeUnlockCondition _unlockCondition;
    private readonly IProgressionProvider _provider;
    private readonly UpgradeUnlockService _unlockService;

    public CreateNodeButtons(
     GameObject nodeUIPrefab,
     Dictionary<Node, GameObject> spawnedNodes,
     NodeTree tree,
     UpgradeUnlockService unlockService)
    {
        _tree = tree;
        _nodeUIPrefab = nodeUIPrefab;
        _spawnedNodes = spawnedNodes;
        _unlockService = unlockService;
    }

    public void Execute(RectTransform container)
    {
        foreach (Node nodeData in _tree.Nodes)
        {
            if (nodeData == null) continue;

            GameObject go = Object.Instantiate(_nodeUIPrefab, container);
            Button button = go.GetComponent<Button>();

            button.onClick.AddListener(() =>
            {
                if (_unlockService.TryUnlock(nodeData))
                {
                    RefreshAllNodes();
                }
            });

            Setup(nodeData, go);
            _spawnedNodes.Add(nodeData, go);
        }

        RefreshAllNodes();
    }

    private void RefreshAllNodes()
    {
        foreach (var kvp in _spawnedNodes)
        {
            Node node = kvp.Key;
            GameObject go = kvp.Value;

            if (!go.TryGetComponent<Button>(out var button))
                continue;

            bool isUnlocked = _unlockService.Provider.IsNodeUnlocked(node.ID);

            bool canUnlock = _unlockService.CanUnlock(node);
            bool isVisible = _unlockService.IsVisible(node);

            go.SetActive(isVisible);
            button.interactable = !isUnlocked && canUnlock;

            UpdateVisual(go, node, isUnlocked, canUnlock);
        }
    }

    private void UpdateVisual(GameObject go, Node node,
        bool isUnlocked, bool canUnlock)
    {
        if (!go.TryGetComponent<Image>(out var frame))
            return;

        if (isUnlocked)
            frame.color = node.UnlockedColor;
        else if (canUnlock)
            frame.color = Color.white;
        else
            frame.color = new Color(0.2f, 0.2f, 0.2f, 1f);
    }

    private void Setup(Node node, GameObject go)
    {
        var text = go.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (text != null)
            text.text = node.Name;

        if (go.TryGetComponent<RectTransform>(out var rect))
        {
            Vector2 pos = node.position;
            pos.y = -pos.y;
            rect.anchoredPosition = pos;
        }
    }
}