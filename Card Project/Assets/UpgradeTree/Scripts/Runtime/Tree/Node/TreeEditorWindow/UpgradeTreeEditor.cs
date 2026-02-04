namespace Eiquif.UpgradeTree.Editor.TreeWindow
{
    using Runtime.Tree;
    using RuntimeNode = Runtime.Node.Node;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEditor.Experimental.GraphView;
    using UnityEditor.UIElements;
    using UnityEngine.UIElements;

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
        private static void Open() => GetWindow<UpgradeTreeEditor>("Upgrade Tree");

        private void OnEnable()
        {
            rootVisualElement.Clear();
            BuildUI();
        }

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
            var bar = new Toolbar();

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

            bar.Add(treeField);
            bar.Add(new Button(CreateNode) { text = "+ NODE" });
            bar.Add(new Button(Reload) { text = "UPDATE" });
            bar.Add(new Button(Save) { text = "SAVE" });

            rootVisualElement.Add(bar);

            var split = new TwoPaneSplitView(0, 300, TwoPaneSplitViewOrientation.Horizontal);
            rootVisualElement.Add(split);

            _graph = new UpgradeGraphView(this)
            {
                style = { flexGrow = 1 },
                OnSelect = DrawNodeInspector
            };
            split.Add(_graph);

            _inspector = new VisualElement();
            var scroll = new ScrollView();
            scroll.Add(_inspector);
            split.Add(scroll);
        }

        #region Graph Callbacks
        public void OnEdgeCreated(Edge edge) => _edgeCreator?.Execute(edge);
        public void OnEdgeRemoved(Edge edge) => _edgeRemover?.Execute(edge);
        public void OnNodeRemoved(UpgradeNodeView view) => _nodeRemover?.Execute(view);
        #endregion

        private void CreateNode()
        {
            _nodeCreator?.Execute();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Reload();
        }

        private void DrawTreeInspector()
        {
            _inspector.Clear();
            if (_tree != null)
                _inspector.Add(new InspectorElement(new SerializedObject(_tree)));
        }

        private void DrawNodeInspector(UpgradeNodeView view)
        {
            _inspector.Clear();
            if (view == null)
                DrawTreeInspector();
            else
                _inspector.Add(new InspectorElement(new SerializedObject(view.Data)));
        }

        private void Reload()
        {
            if (_tree == null) return;

            _graph.ClearGraph();
            Normalize(_tree);

            var map = new Dictionary<RuntimeNode, UpgradeNodeView>();

            foreach (var node in _tree.Nodes)
            {
                if (node == null) continue;

                var view = new UpgradeNodeView(node);
                _graph.AddElement(view);
                map[node] = view;
            }

            foreach (var node in _tree.Nodes)
            {
                if (node == null) continue;

                foreach (var next in node.NextNodes)
                {
                    if (next != null && map.ContainsKey(next))
                        _graph.AddElement(map[node].Out.ConnectTo(map[next].In));
                }
            }
        }

        private void Save()
        {
            if (_tree == null) return;

            EditorUtility.SetDirty(_tree);

            foreach (var node in _tree.Nodes)
            {
                if (node != null)
                    EditorUtility.SetDirty(node);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void Normalize(NodeTree tree)
        {
            tree.Nodes = tree.Nodes
                .Where(n => n != null)
                .Distinct()
                .ToList();

            foreach (var n in tree.Nodes)
                n.PrerequisiteNodes.Clear();

            foreach (var n in tree.Nodes)
                foreach (var next in n.NextNodes)
                    if (next != null && !next.PrerequisiteNodes.Contains(n))
                        next.PrerequisiteNodes.Add(n);
        }
    }
}
