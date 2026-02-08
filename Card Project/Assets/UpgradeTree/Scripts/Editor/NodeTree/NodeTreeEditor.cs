//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
// Description: Shows all properties and data of Scriptable Object - NodeTree
//              on inspector screen
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    [CustomEditor(typeof(NodeTree))]
    public class NodeTreeEditor : UnityEditor.Editor
    {
        private NodeTree _tree;

        private NodeTreeContext _context;

        #region Section
        private EditorNames _names;

        private Section _node;
        private Section _summary;
        private Section _id;
        #endregion

        private void OnEnable()
        {
            _tree = (NodeTree)target;

            _context = new NodeTreeContext(_tree, serializedObject);

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