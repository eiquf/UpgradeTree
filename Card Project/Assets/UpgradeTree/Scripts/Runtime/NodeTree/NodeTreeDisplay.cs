//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System.Collections.Generic;
using UnityEngine;

namespace Eiquif.UpgradeTree.Runtime
{
    /// <summary>
    /// Runtime system responsible for visualizing an upgrade tree.
    /// </summary>
    public class NodeTreeDisplay : NodeTreeRuntimeSystem
    {
        private readonly GameObject _nodeUIPrefab;
        private readonly RectTransform _container;

        private readonly Dictionary<Node, GameObject> _spawnedNodes = new();

        private readonly UpgradeUnlockService _unlockService;

        private readonly IElement<RectTransform> _createNodeButtons;
        private readonly IElement<List<GameObject>> _createConnectionLine;

        public NodeTreeDisplay(
            NodeTree tree,
            GameObject nodeUIPrefab,
            RectTransform container,
            UpgradeUnlockService unlockService)
            : base(tree)
        {
            _nodeUIPrefab = nodeUIPrefab;
            _container = container;
            _unlockService = unlockService;

            _createNodeButtons = new CreateNodeButtons(
                _nodeUIPrefab,
                _spawnedNodes,
                Tree,
                _unlockService);

            _createConnectionLine = new CreateNodeLine();
        }

        public override void Execute()
        {
            ClearContainer();
            CreateNodes();
            CreateConnections();
        }

        private void ClearContainer()
        {
            foreach (Transform child in _container)
                Object.Destroy(child.gameObject);

            _spawnedNodes.Clear();
        }

        private void CreateNodes()
        {
            _createNodeButtons.Execute(_container);
        }

        private void CreateConnections()
        {
            foreach (Node node in Tree.Nodes)
            {
                if (node == null) continue;

                foreach (Node nextNode in node.NextNodes)
                {
                    if (nextNode == null) continue;

                    if (!_spawnedNodes.TryGetValue(node, out var from)) continue;
                    if (!_spawnedNodes.TryGetValue(nextNode, out var to)) continue;

                    CreateLine(from, to);
                }
            }
        }

        private void CreateLine(GameObject from, GameObject to)
        {
            _createConnectionLine.Execute(new List<GameObject>
            {
                from,
                to
            });
        }
    }
}
