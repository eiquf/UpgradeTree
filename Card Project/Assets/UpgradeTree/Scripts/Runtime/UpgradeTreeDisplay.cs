//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System.Collections.Generic;
using UnityEngine;

namespace Eiquif.UpgradeTree.Runtime
{
    public class UpgradeTreeDisplay : UpgradeTreeRuntimeSystem
    {
        private readonly GameObject _nodeUIPrefab;
        private readonly RectTransform _container;

        private readonly Dictionary<Node, GameObject> _spawnedNodes = new();

        private readonly IElement<RectTransform> _createNodeButtons;
        private readonly IElement<List<GameObject>> _createConnectionLine;

        public UpgradeTreeDisplay(
            NodeTree tree,
            GameObject nodeUIPrefab,
            RectTransform container)
            : base(tree)
        {
            _nodeUIPrefab = nodeUIPrefab;
            _container = container;

            _createNodeButtons = new CreateNodeButtons(
                _nodeUIPrefab,
                _spawnedNodes,
                Tree);

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
                foreach (Node nextNode in node.NextNodes)
                {
                    CreateLine(
                        _spawnedNodes[node],
                        _spawnedNodes[nextNode]);
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