using UnityEditor;
using UnityEngine;
using UpgradeTree.Nodes;

namespace Assets.Scripts
{
    /// <summary>
    /// Custom editor for the NodeTree ScriptableObject.
    /// Odin Inspector-style UI with professional look and feel.
    /// </summary>
    [CustomEditor(typeof(NodeTree))]
    public class NodeTreeEditor : Editor
    {
        private SerializedProperty nodesProp;
        private SerializedProperty idsProp;

        private NodeTree tree;

        private Vector2 scrollPosition;

        private double _lastUpdateTime;

        private NodeTreeContext _context;
        private EditorNames _names;

        private Section _node;
        private Section _action;
        private Section _summary;
        private Section _id;

        private void OnEnable()
        {
            tree = (NodeTree)target;

            nodesProp = serializedObject.FindProperty("Nodes");
            idsProp = serializedObject.FindProperty("IDs");

            var SO = new SerializedObject(this);
            _context = new(SO, nodesProp, idsProp, tree);

            _node = new NodeSection(_context);
            _action = new ActionsSection(_context);
            _summary = new SummarySection(_context);
            _id = new IDSection(_context);

            _names = new NodeTreeEditorNames(_context, tree.name, _lastUpdateTime);
        }

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
}
