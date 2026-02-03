using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    private SerializedProperty _nextProp;
    private SerializedProperty _prerequisiteProp;

    private SerializedObject _so;

    private Node _node;
    private NodeContext _ctx;

    private NodeEditorNames _names;
    private NodeInfoSection _info;
    private NodeGraphSection _graph;
    private NodeRequirementsSection _requirements;

    private double _lastUpdateTime;
    private void OnEnable()
    {
        _node = (Node)target;
        _so = new SerializedObject(target);

        _nextProp = serializedObject.FindProperty("NextNodes");
        _prerequisiteProp = serializedObject.FindProperty("PrerequisiteNodes");

        _ctx = new NodeContext(_so, _node, _nextProp, _prerequisiteProp);

        _graph = new NodeGraphSection(_ctx);
        _names = new(_ctx, _node.name);

        _info = new NodeInfoSection(serializedObject, _ctx);
        _requirements = new NodeRequirementsSection(serializedObject);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.BeginVertical();

        UpdateTime();
        Draw();

        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }

    private void Draw()
    {
        GUILayout.Space(8);
        _names.DrawHeader();

        GUILayout.Space(12);
        _info.Draw();

        GUILayout.Space(8);
        _requirements.Draw();

        GUILayout.Space(8);
        _graph.Draw();

        _names.DrawFooter();
    }
    private void UpdateTime()
    {
        if (_lastUpdateTime == 0)
        {
            _lastUpdateTime = EditorApplication.timeSinceStartup;
            _ctx.UpdateTime(_lastUpdateTime);
        }
    }
}
