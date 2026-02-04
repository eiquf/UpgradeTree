namespace Eiquif.UpgradeTree.Editor.Tree
{
    using Eiquif.UpgradeTree.Editor.Node;
    using Runtime.Node;
    using Runtime.Tree;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(NodeTree))]
    public class NodeTreeEditor : Editor
    {
        private SerializedProperty _nodesProp;
        private SerializedProperty _idsProp;

        private NodeTree _tree;

        private NodeTreeContext _context;

        private EditorNames _names;

        #region Section
        private Section _node;
        private Section _summary;
        private Section _id;
        #endregion

        private void OnEnable()
        {
            _tree = (NodeTree)target;

            _nodesProp = serializedObject.FindProperty("Nodes");
            _idsProp = serializedObject.FindProperty("IDs");

            var SO = new SerializedObject(this);
            _context = new NodeTreeContext(_tree, SO, _nodesProp, _idsProp);

            _node = new NodeSection(_context);
            _summary = new SummarySection(_context);
            _id = new IDSection(_context);

            _names = new NodeTreeEditorNames(_context, _tree.name);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical();
            GUILayout.Space(8);

            _names.DrawHeader();

            GUILayout.Space(12);

            _id.Draw();
            GUILayout.Space(8);

            _node.Draw();
            GUILayout.Space(8);

            _summary.Draw();
            GUILayout.Space(12);

            _names.DrawFooter();

            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}