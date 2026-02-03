using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeTreeEditor : EditorWindow
{
    UpgradeGraphView graph;
    VisualElement inspector;
    NodeTree tree;

    [MenuItem("Window/UpgradeTree/Editor")]
    static void Open() => GetWindow<UpgradeTreeEditor>("Upgrade Tree");

    void OnEnable()
    {
        rootVisualElement.Clear();
        BuildUI();
    }

    void BuildUI()
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

        graph = new UpgradeGraphView { style = { flexGrow = 1 } };
        graph.Editor = this;
        graph.OnSelect = DrawNodeInspector;
        split.Add(graph);

        inspector = new VisualElement();
        inspector = new VisualElement();

        var scroll = new ScrollView();
        scroll.Add(inspector);

        split.Add(scroll);

    }

    // ================= CREATE NODE =================
    void CreateNode()
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

    // ================= RELOAD =================
    void Reload()
    {
        if (tree == null) return;

        graph.ClearGraph();
        Normalize(tree);

        var map = new Dictionary<Node, UpgradeNodeView>();

        foreach (var n in tree.Nodes)
        {
            var v = new UpgradeNodeView(n);
            graph.AddElement(v);
            map[n] = v;
        }

        foreach (var n in tree.Nodes)
            foreach (var next in n.NextNodes)
                if (next != null)
                    graph.AddElement(map[n].Out.ConnectTo(map[next].In));
    }

    void Save()
    {
        if (tree == null) return;
        EditorUtility.SetDirty(tree);
        AssetDatabase.SaveAssets();
    }

    // ================= EDGES =================
    public void OnEdgeCreated(Edge edge)
    {
        var from = edge.output.node as UpgradeNodeView;
        var to = edge.input.node as UpgradeNodeView;
        if (from == null || to == null) return;

        Undo.RecordObjects(
            new Object[] { from.Data, to.Data, tree },
            "Add Edge"
        );

        if (!from.Data.NextNodes.Contains(to.Data))
            from.Data.NextNodes.Add(to.Data);

        if (!to.Data.PrerequisiteNodes.Contains(from.Data))
            to.Data.PrerequisiteNodes.Add(from.Data);

        EditorUtility.SetDirty(from.Data);
        EditorUtility.SetDirty(to.Data);
        EditorUtility.SetDirty(tree);
    }

    public void OnEdgeRemoved(Edge edge)
    {
        var from = edge.output.node as UpgradeNodeView;
        var to = edge.input.node as UpgradeNodeView;
        if (from == null || to == null) return;

        Undo.RecordObjects(
            new Object[] { from.Data, to.Data, tree },
            "Remove Edge"
        );

        from.Data.NextNodes.Remove(to.Data);
        to.Data.PrerequisiteNodes.Remove(from.Data);

        EditorUtility.SetDirty(from.Data);
        EditorUtility.SetDirty(to.Data);
        EditorUtility.SetDirty(tree);
    }

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

    // ================= INSPECTOR =================
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

    // ================= NORMALIZE =================
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
