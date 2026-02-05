using Eiquif.UpgradeTree.Editor;
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
        private Dictionary<Node, GameObject> _spawnedNodes;

        public CreateNodeButtons(
            GameObject nodeUIPrefab,
            Dictionary<Node, GameObject> spawnedNodes,
            NodeTree tree)
        {
            _tree = tree;
            _nodeUIPrefab = nodeUIPrefab;
            _spawnedNodes = spawnedNodes;
        }
        public void Execute(RectTransform container)
        {
            foreach (Node nodeData in _tree.Nodes)
            {
                if (nodeData == null) continue;

                GameObject go = Object.Instantiate(_nodeUIPrefab, container);
                Button button = go.GetComponent<Button>();
                //button.onClick.AddListener();

                Setup(nodeData, go);
                _spawnedNodes.Add(nodeData, go);
            }
        }
        private void Setup(Node data, GameObject pref)
        {
            if (pref.TryGetComponent<Image>(out var icon))
                icon.sprite = data.Icon;

            if (pref.TryGetComponent<TextMeshProUGUI>(out var titleText))
                titleText.text = data.Name;

            Vector2 pos = data.position;
            pos.y = -pos.y;

            if (pref.TryGetComponent<RectTransform>(out var rect))
                rect.anchoredPosition = pos;
        }
    }
}
