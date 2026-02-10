//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Runtime system responsible for visualizing an upgrade tree.
/// </summary>
/// <remarks>
/// Handles spawning node UI elements, clearing and rebuilding the layout,
/// and drawing visual connections between nodes based on tree data.
/// </remarks>
namespace Eiquif.UpgradeTree.Runtime
{
    public class NodeTreeDisplay : NodeTreeRuntimeSystem
    {
        /// <summary>
        /// Prefab used to instantiate node UI elements.
        /// </summary>
        private readonly GameObject _nodeUIPrefab;

        /// <summary>
        /// RectTransform that acts as the parent container for all node UI elements.
        /// </summary>
        private readonly RectTransform _container;

        /// <summary>
        /// Mapping between runtime nodes and their spawned UI GameObjects.
        /// </summary>
        private readonly Dictionary<Node, GameObject> _spawnedNodes = new();

        /// <summary>
        /// Element responsible for creating node UI buttons.
        /// </summary>
        private readonly IElement<RectTransform> _createNodeButtons;

        /// <summary>
        /// Element responsible for creating visual connection lines between nodes.
        /// </summary>
        private readonly IElement<List<GameObject>> _createConnectionLine;

        /// <summary>
        /// Initializes a new upgrade tree display system.
        /// </summary>
        /// <param name="tree">Upgrade tree data used for visualization.</param>
        /// <param name="nodeUIPrefab">Prefab for a single node UI.</param>
        /// <param name="container">UI container where nodes will be spawned.</param>
        public NodeTreeDisplay(
            NodeTree tree,
            GameObject nodeUIPrefab,
            RectTransform container,
            INodeUnlockCondition condition,
            IProgressionProvider progression)
            : base(tree)
        {
            _nodeUIPrefab = nodeUIPrefab;
            _container = container;

            _createNodeButtons = new CreateNodeButtons(
                _nodeUIPrefab,
                _spawnedNodes,
                Tree,
                condition,
                progression);

            _createConnectionLine = new CreateNodeLine();
        }

        /// <summary>
        /// Rebuilds the entire visual representation of the upgrade tree.
        /// </summary>
        public override void Execute()
        {
            ClearContainer();
            CreateNodes();
            CreateConnections();
        }

        /// <summary>
        /// Removes all spawned node UI elements from the container.
        /// </summary>
        private void ClearContainer()
        {
            foreach (Transform child in _container)
                Object.Destroy(child.gameObject);

            _spawnedNodes.Clear();
        }

        /// <summary>
        /// Creates UI elements for all nodes in the tree.
        /// </summary>
        private void CreateNodes()
        {
            _createNodeButtons.Execute(_container);
        }

        /// <summary>
        /// Creates visual connections between nodes based on tree relationships.
        /// </summary>
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

        /// <summary>
        /// Creates a visual line connecting two node UI elements.
        /// </summary>
        /// <param name="from">Source node UI.</param>
        /// <param name="to">Target node UI.</param>
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