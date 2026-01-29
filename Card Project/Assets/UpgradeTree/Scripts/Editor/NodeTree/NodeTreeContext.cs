using UnityEditor;
using UnityEngine;

/// <summary>
/// The context for a NodeTree editor, providing access to the <see cref="NodeTree"/> instance,
/// its serialized properties, and methods for recording changes for undo functionality.
/// </summary>
public class NodeTreeContext : ContextSystem
{
    public NodeTreeContext(NodeTree tree, SerializedObject so, SerializedProperty nodes, SerializedProperty ids)
    {
        Tree = tree;
        SerializedObject = so;

        NodesProp = nodes;
        IDsProp = ids;

        // Log errors if any serialized properties are missing
        if (IDsProp == null)
            Debug.LogError("[NodeTreeEditor] idsProp is null!");

        if (NodesProp == null)
            Debug.LogError("[NodeTreeEditor] nodesProp is null!");
    }

    /// <summary>
    /// The <see cref="NodeTree"/> ScriptableObject that this context operates on.
    /// </summary>
    public NodeTree Tree;
    /// <summary>
    /// The <see cref="SerializedObject"/> representing the serialized data of the <see cref="NodeTree"/>.
    /// </summary>
    public SerializedObject SerializedObject { get; }

    /// <summary>
    /// Serialized property representing the list of nodes in the NodeTree.
    /// </summary>
    public SerializedProperty NodesProp { get; }

    /// <summary>
    /// Serialized property representing the list of IDs in the NodeTree.
    /// </summary>
    public SerializedProperty IDsProp { get; }
    public double LastUpdateTime { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NodeTreeContext"/> class.
    /// </summary>
    /// <param name="so">The <see cref="SerializedObject"/> that represents the NodeTree's serialized data.</param>
    /// <param name="nodes">The <see cref="SerializedProperty"/> for the list of nodes in the NodeTree.</param>
    /// <param name="ids">The <see cref="SerializedProperty"/> for the list of IDs in the NodeTree.</param>
    /// <param name="tree">The <see cref="NodeTree"/> ScriptableObject that this context is associated with.</param>

    /// <summary>
    /// Records changes to the <see cref="NodeTree"/> for undo functionality.
    /// This allows Unity's undo system to track changes made to the tree.
    /// </summary>
    /// <param name="label">The label describing the action to be undone.</param>
    public void Record(string label)
    {
        Undo.RecordObject(Tree, label);
        EditorUtility.SetDirty(Tree);
    }

    public void UpdateTime(double time) => LastUpdateTime = time;

}
