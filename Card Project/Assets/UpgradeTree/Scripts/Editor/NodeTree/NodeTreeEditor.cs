using UnityEditor;
using UnityEngine;


/// <summary>
/// Custom editor for the <see cref="NodeTree"/> ScriptableObject, providing a custom inspector interface.
/// </summary>
[CustomEditor(typeof(NodeTree))]
public class NodeTreeEditor : Editor
{
    /// <summary>
    /// Serialized property representing the lists in the NodeTree.
    /// </summary>
    private SerializedProperty _nodesProp;
    private SerializedProperty _idsProp;

    /// <summary>
    /// Reference to the target <see cref="NodeTree"/> ScriptableObject.
    /// </summary>
    private NodeTree _tree;

    /// <summary>
    /// Timestamp of the last update to the editor window.
    /// </summary>
    private double _lastUpdateTime;

    /// <summary>
    /// Context containing relevant data for drawing sections in the editor.
    /// </summary>
    private NodeTreeContext _context;

    /// <summary>
    /// Container for storing the names used throughout the editor interface.
    /// </summary>
    private EditorNames _names;

    /// <summary>
    /// Section for displaying node-related information in the custom editor.
    /// </summary>
    private Section _node;
    private Section _action;
    private Section _summary;
    private Section _id;

    /// <summary>
    /// Initializes the custom editor, setting up serialized properties and sections.
    /// </summary>
    private void OnEnable()
    {
        // Cast the target to the appropriate type
        _tree = (NodeTree)target;

        // Find the serialized properties for Nodes and IDs
        _nodesProp = serializedObject.FindProperty("Nodes");
        _idsProp = serializedObject.FindProperty("IDs");

        // Create a new serialized object for the editor context
        var SO = new SerializedObject(this);
        _context = new NodeTreeContext(_tree, SO, _nodesProp, _idsProp);

        _node = new NodeSection(_context);
        _action = new ActionsSection(_context);
        _summary = new SummarySection(_context);
        _id = new IDSection(_context);

        _names = new NodeTreeEditorNames(_context, _tree.name);
    }

    /// <summary>
    /// Draws the custom inspector GUI for the <see cref="NodeTree"/>.
    /// </summary>
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (_lastUpdateTime == 0)
            _lastUpdateTime = EditorApplication.timeSinceStartup;

        EditorGUILayout.BeginVertical();
        GUILayout.Space(8);

        _names.DrawHeader();

        GUILayout.Space(12);

        _id.Draw();
        GUILayout.Space(8);

        _node.Draw();
        GUILayout.Space(8);

        _action.Draw();
        GUILayout.Space(8);

        _summary.Draw();
        GUILayout.Space(12);

        _names.DrawFooter();

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}