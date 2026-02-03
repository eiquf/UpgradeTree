using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTreeDisplay : UpgradeTreeRuntimeSystem
{
    private readonly GameObject _nodeUIPrefab;
    private readonly RectTransform _container;

    private readonly Dictionary<Node, UpgradeNodeUI> _spawnedNodes = new();

    public UpgradeTreeDisplay(NodeTree tree, GameObject pref, RectTransform container) : base(tree)
    {
        _nodeUIPrefab = pref;
        _container = container;
    }

    public override void Execute()
    {
        foreach (Transform child in _container) Object.Destroy(child.gameObject);

        _spawnedNodes.Clear();

        CreatingButtons();

        foreach (Node nodeData in Tree.Nodes)
        {
            foreach (Node nextNode in nodeData.NextNodes)
            {
                CreateLine(_spawnedNodes[nodeData], _spawnedNodes[nextNode]);
            }
        }
    }
    private void CreatingButtons()
    {
        foreach (Node nodeData in Tree.Nodes)
        {
            if (nodeData == null) continue;

            GameObject go = Object.Instantiate(_nodeUIPrefab, _container);
            Button button = go.GetComponent<Button>();
            //button.onClick.AddListener();

            UpgradeNodeUI uiScript = go.GetComponent<UpgradeNodeUI>();
            uiScript.Setup(nodeData);

            _spawnedNodes.Add(nodeData, uiScript);
        }
    }
    private void CreateLine(UpgradeNodeUI from, UpgradeNodeUI to)
    {
        GameObject lineObj = new("ConnectLine", typeof(Image));
        lineObj.transform.SetParent(_container, false);
        lineObj.transform.SetAsFirstSibling();

        Image lineImage = lineObj.GetComponent<Image>();
        lineImage.color = Color.white;

        RectTransform rect = lineObj.GetComponent<RectTransform>();
        RectTransform fromRect = from.GetComponent<RectTransform>();
        RectTransform toRect = to.GetComponent<RectTransform>();

        Vector2 startPos = fromRect.anchoredPosition;
        Vector2 endPos = toRect.anchoredPosition;
        Vector2 direction = endPos - startPos;
        float distance = direction.magnitude;

        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0, 0.5f);

        rect.anchoredPosition = startPos;
        rect.sizeDelta = new Vector2(distance, 8f);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rect.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
