//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//
// Description:
// Custom Unity Editor window for creating and editing Upgrade Trees.
// Provides a visual graph-based interface to:
//  - Create and remove upgrade nodes
//  - Connect nodes with directional edges
//  - Edit tree and node data via an inspector panel
//
// Acts as the main orchestration layer between GraphView UI,
// ScriptableObject data, and editor commands.
//***************************************************************************************

using Eiquif.UpgradeTree.Runtime;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using RuntimeNode = Eiquif.UpgradeTree.Runtime.Node;

namespace Eiquif.UpgradeTree.Editor
{
    public class UpgradeTreeEditor : EditorWindow
    {
        private UpgradeGraphView _graph;
        private VisualElement _inspector;
        private NodeTree _tree;

        private IElement<Edge> _edgeCreator;
        private IElement<Edge> _edgeRemover;
        private IElement<UpgradeNodeView> _nodeRemover;
        private IElement _nodeCreator;

        [MenuItem("Window/UpgradeTree/Editor")]
        private static void Open() =>
            GetWindow<UpgradeTreeEditor>("Upgrade Tree");

        private void OnEnable()
        {
            rootVisualElement.Clear();
            BuildUI();

            Undo.undoRedoPerformed += OnExternalDataChanged;
        }

        private void OnDisable()
        {
            Undo.undoRedoPerformed -= OnExternalDataChanged;
        }

        private void OnExternalDataChanged()
        {
            if (_tree == null) return;

            EditorApplication.delayCall += () =>
            {
                if (this == null) return;
                Reload();
                DrawTreeInspector();
            };
        }
        private void MarkDirtyAndReload()
        {
            if (_tree == null) return;

            EditorUtility.SetDirty(_tree);
            Reload();
        }
        public void ForceReloadFromAsset(NodeTree tree)
        {
            if (_tree != tree) return;
            Reload();
            DrawTreeInspector();
        }


        #region Initialization

        private void Init()
        {
            if (_tree == null) return;

            _nodeCreator = new CreateNode(_tree);
            _nodeRemover = new RemoveNode(_tree);
            _edgeCreator = new CreateEdge(_tree);
            _edgeRemover = new RemoveEdge(_tree);
        }

        private void BuildUI()
        {
            var toolbar = new Toolbar();

            var treeField = new ObjectField("Tree")
            {
                objectType = typeof(NodeTree)
            };

            treeField.RegisterValueChangedCallback(e =>
            {
                _tree = e.newValue as NodeTree;
                Init();
                Reload();
                DrawTreeInspector();
            });

            toolbar.Add(treeField);
            toolbar.Add(new Button(CreateNode) { text = "+ NODE" });
            toolbar.Add(new Button(Save) { text = "SAVE" });

            rootVisualElement.Add(toolbar);

            var splitView = new TwoPaneSplitView(
                0, 300, TwoPaneSplitViewOrientation.Horizontal);

            rootVisualElement.Add(splitView);

            _graph = new UpgradeGraphView(this)
            {
                style = { flexGrow = 1 },
                OnSelect = DrawNodeInspector
            };

            splitView.Add(_graph);

            _inspector = new VisualElement();
            splitView.Add(_inspector);
        }

        #endregion

        #region Graph Callbacks

        public void OnEdgeCreated(Edge edge)
        {
            _edgeCreator?.Execute(edge);
            MarkDirtyAndReload();
        }

        public void OnEdgeRemoved(Edge edge)
        {
            _edgeRemover?.Execute(edge);
            MarkDirtyAndReload();
        }

        public void OnNodeRemoved(UpgradeNodeView view)
        {
            _nodeRemover?.Execute(view);
            MarkDirtyAndReload();
        }

        #endregion

        #region UI Actions

        private void CreateNode()
        {
            _nodeCreator?.Execute();
            Save();
            Reload();
        }

        private void Reload()
        {
            if (_tree == null) return;

            _graph.ClearGraph();
            Normalize(_tree);

            var nodeViewMap =
                new Dictionary<RuntimeNode, UpgradeNodeView>();

            foreach (var node in _tree.Nodes)
            {
                if (node == null) continue;

                var view = new UpgradeNodeView(node);
                _graph.AddElement(view);
                nodeViewMap[node] = view;
            }

            foreach (var node in _tree.Nodes)
            {
                if (node == null) continue;

                foreach (var next in node.NextNodes)
                {
                    if (next != null &&
                        nodeViewMap.ContainsKey(next))
                    {
                        _graph.AddElement(
                            nodeViewMap[node].Out
                                .ConnectTo(nodeViewMap[next].In));
                    }
                }
            }
        }

        private void Save()
        {
            EditorUtility.SetDirty(_tree);
            AssetDatabase.SaveAssets();
        }

        #endregion

        #region Inspector

        private void DrawTreeInspector()
        {
            _inspector.Clear();

            if (_tree != null)
                _inspector.Add(
                    new InspectorElement(new SerializedObject(_tree)));
        }

        private void DrawNodeInspector(UpgradeNodeView view)
        {
            _inspector.Clear();

            if (view == null)
                DrawTreeInspector();
            else
                _inspector.Add(
                    new InspectorElement(
                        new SerializedObject(view.Data)));
        }

        #endregion

        #region Data Normalization

        private static void Normalize(NodeTree tree)
        {
            tree.Nodes = tree.Nodes
                .Where(n => n != null)
                .Distinct()
                .ToList();

            foreach (var node in tree.Nodes)
                node.PrerequisiteNodes.Clear();

            foreach (var node in tree.Nodes)
            {
                foreach (var next in node.NextNodes)
                {
                    if (next != null &&
                        !next.PrerequisiteNodes.Contains(node))
                    {
                        next.PrerequisiteNodes.Add(node);
                    }
                }
            }
        }

        #endregion
    }
}