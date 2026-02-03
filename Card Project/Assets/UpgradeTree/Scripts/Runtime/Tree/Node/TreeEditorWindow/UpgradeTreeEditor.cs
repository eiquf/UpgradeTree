using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


namespace Eiquif.UpgradeTree.Editor.TreeWindow
{
    public class UpgradeTreeEditor : EditorWindow
    {
        private UpgradeGraphView _graph;
        VisualElement inspector;
        NodeTree tree;

        private IElement<Edge> _edgeCreator;
        private IElement<Edge> _edgeRemover;


        [MenuItem("Window/UpgradeTree/Editor")]
        private static void Open() => GetWindow<UpgradeTreeEditor>("Upgrade Tree");

        private void OnEnable()
        {
            _edgeCreator = new CreateEdge(tree);
            rootVisualElement.Clear();
            BuildUI();
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
                tree = e.newValue as NodeTree;
                Reload();
                DrawTreeInspector();
            });

            bar.Add(treeField);
            bar.Add(new Button(CreateNode) { text = "+ NODE" });
            bar.Add(new Button(Reload) { text = "RELOAD" });
            bar.Add(new Button(Save) { text = "SAVE" });

            rootVisualElement.Add(bar);

            var split = new TwoPaneSplitView(0, 300, TwoPaneSplitViewOrientation.Horizontal);
            rootVisualElement.Add(split);

            _graph = new UpgradeGraphView(this)
            { 

                style = { flexGrow = 1 } 
            
            };
            _graph.OnSelect = DrawNodeInspector;
            split.Add(_graph);

            inspector = new VisualElement();

            var scroll = new ScrollView();
            scroll.Add(inspector);

            split.Add(scroll);

        }
        public void OnEdgeCreated(Edge edge) => _edgeCreator.Execute(edge);
        public void OnEdgeRemoved(Edge edge) => _edgeRemover.Execute(edge);

        // ================= REMOVE NODE =================
        public void OnNodeRemoved(UpgradeNodeView view)
        {
            var node = view.Data;

            Undo.RecordObject(tree, "Remove Node");

            tree.Nodes.Remove(node);

            foreach (var n in tree.Nodes)
            {
                n.NextNodes.Remove(node);
                n.PrerequisiteNodes.Remove(node);
                EditorUtility.SetDirty(n);
            }

            AssetDatabase.RemoveObjectFromAsset(node);
            DestroyImmediate(node, true);

            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();
        }
        private void CreateNode()
        {
            if (tree == null) return;

            Undo.RecordObject(tree, "Create Node");

            var node = ScriptableObject.CreateInstance<Node>();
            node.name = "Node";

            tree.Nodes.Add(node);
            AssetDatabase.AddObjectToAsset(node, tree);

            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();

            Reload();
        }

        private void Reload()
        {
            if (tree == null) return;

            _graph.ClearGraph();
            Normalize(tree);

            var map = new Dictionary<Node, UpgradeNodeView>();

            foreach (var n in tree.Nodes)
            {
                var v = new UpgradeNodeView(n);
                _graph.AddElement(v);
                map[n] = v;
            }

            foreach (var n in tree.Nodes)
                foreach (var next in n.NextNodes)
                    if (next != null)
                        _graph.AddElement(map[n].Out.ConnectTo(map[next].In));
        }

        private void Save()
        {
            if (tree == null) return;
            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();
        }
        void DrawTreeInspector()
        {
            inspector.Clear();
            if (tree != null)
                inspector.Add(new InspectorElement(new SerializedObject(tree)));
        }

        void DrawNodeInspector(UpgradeNodeView view)
        {
            inspector.Clear();
            if (view == null)
                DrawTreeInspector();
            else
                inspector.Add(new InspectorElement(new SerializedObject(view.Data)));
        }

        static void Normalize(NodeTree tree)
        {
            tree.Nodes = tree.Nodes.Where(n => n != null).Distinct().ToList();

            foreach (var n in tree.Nodes)
                n.PrerequisiteNodes.Clear();

            foreach (var n in tree.Nodes)
                foreach (var next in n.NextNodes)
                    if (next != null && !next.PrerequisiteNodes.Contains(n))
                        next.PrerequisiteNodes.Add(n);
        }
    }
}

interface IElement<T>
{
    void Execute(T t);
}
