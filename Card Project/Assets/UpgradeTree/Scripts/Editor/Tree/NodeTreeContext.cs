using UnityEditor;
using UnityEngine;
using UpgradeTree.Nodes;

public class NodeTreeContext
{
    public NodeTree Tree { get; }
    public SerializedObject SerializedObject { get; }

    public SerializedProperty NodesProp { get; }
    public SerializedProperty IDsProp { get; }

    public NodeTreeContext(SerializedObject so, SerializedProperty nodes, SerializedProperty ids, NodeTree tree)
    {
        Tree = tree;
        SerializedObject = so;

        NodesProp = nodes;
        IDsProp = ids;

        if (IDsProp == null)
            Debug.LogError("[NodeTreeEditor] idsProp is null!");
        if (NodesProp == null)
            Debug.LogError("[NodeTreeEditor] nodesProp is null!");
    }

    public void Record(string label)
    {
        Undo.RecordObject(Tree, label);
        EditorUtility.SetDirty(Tree);
    }
}