//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Eiquif.UpgradeTree.Runtime
{
    public class CreateNodeButtons : IElement<RectTransform>
    {
        private readonly NodeTree _tree;
        private readonly GameObject _nodeUIPrefab;
        private readonly Dictionary<Node, GameObject> _spawnedNodes;
        private readonly Dictionary<Node, bool> _nodeStates;
        private readonly INodeUnlockCondition _unlockCondition;
        private readonly IProgressionProvider _progression;

        public CreateNodeButtons(
            GameObject nodeUIPrefab,
            Dictionary<Node, GameObject> spawnedNodes,
            NodeTree tree,
            INodeUnlockCondition condition,
            IProgressionProvider progression)
        {
            _tree = tree;
            _nodeUIPrefab = nodeUIPrefab;
            _spawnedNodes = spawnedNodes;
            _nodeStates = new Dictionary<Node, bool>();
            _unlockCondition = condition;
            _progression = progression;
        }

        public void Execute(RectTransform container)
        {
            foreach (Node nodeData in _tree.Nodes)
            {
                if (nodeData == null) continue;

                GameObject go = UnityEngine.Object.Instantiate(_nodeUIPrefab, container);
                Button button = go.GetComponent<Button>();

                bool canUnlock = _unlockCondition.CanUnlock(nodeData, _progression);
                bool isVisible = _unlockCondition.IsVisible(nodeData, _progression);

                button.interactable = canUnlock;


                button.onClick.AddListener(() =>
                {
                    if (_unlockCondition.CanUnlock(nodeData, _progression))
                    {
                        ActionRegistry.Invoke(nodeData);

                        if (_progression is LocalProgressionService service)
                            service.UnlockNode(nodeData);

                        RefreshAllNodes();
                    }
                });

                bool isUnlocked = _progression.IsNodeUnlocked(nodeData.ID);
                if (isUnlocked)
                {
                    button.interactable = false;
                }

                Setup(nodeData, go);

                go.SetActive(isVisible);

                _spawnedNodes.Add(nodeData, go);
                _nodeStates[nodeData] = canUnlock;
            }
            RefreshAllNodes();
        }
        private void Setup(Node data, GameObject pref)
        {
            Transform iconTransform = pref.transform.Find("Icon");
            if (iconTransform != null && iconTransform.TryGetComponent<Image>(out var icon))
            {
                icon.sprite = data.Icon;
            }

            var text = pref.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null) text.text = data.Name;

            if (pref.TryGetComponent<RectTransform>(out var rect))
            {
                Vector2 pos = data.position;
                pos.y = -pos.y;
                rect.anchoredPosition = pos;
            }
        }
        private void RefreshAllNodes()
        {
            foreach (var kvp in _spawnedNodes)
            {
                Node nodeData = kvp.Key;
                GameObject go = kvp.Value;

                if (go.TryGetComponent<Button>(out var button))
                {
                    bool isUnlocked = _progression.IsNodeUnlocked(nodeData.ID);
                    bool canUnlock = _unlockCondition.CanUnlock(nodeData, _progression);
                    bool isVisible = _unlockCondition.IsVisible(nodeData, _progression);

                    go.SetActive(isVisible);

                    button.interactable = !isUnlocked && canUnlock;

                    UpdateNodeVisualState(go, isUnlocked, canUnlock, nodeData);
                }
            }
        }
        private void UpdateNodeVisualState(GameObject go, bool isUnlocked, bool canUnlock, Node nodeData)
        {
            if (go.TryGetComponent<Image>(out var frame))
            {
                if (isUnlocked)
                    frame.color = nodeData.UnlockedColor;
                else if (canUnlock)
                    frame.color = Color.white;
                else
                    frame.color = new Color(0.2f, 0.2f, 0.2f, 1f);

                var c = frame.color;
                c.a = 1f;
                frame.color = c;
            }
        }
    }
}