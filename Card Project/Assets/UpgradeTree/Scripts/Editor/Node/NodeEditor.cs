//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    [CustomEditor(typeof(Node))]
    public class NodeEditor : UnityEditor.Editor
    {
        private SerializedObject _so;

        private Node _node;
        private NodeContext _ctx;

        #region Sections
        private NodeEditorNames _names;
        private NodeInfoSection _info;
        private NodeGraphSection _graph;
        private NodeRequirementsSection _requirements;
        #endregion

        private double _lastUpdateTime;
        private void OnEnable() => Init();
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
        #region Initilaize
        private void Init()
        {
            InitProperties();
            _ctx = new NodeContext(_so, _node);
            InitSections();
        }
        private void InitProperties()
        {
            if (target == null) return; 

            _node = target as Node;
            if (_node == null) return; 

            _so = serializedObject;
        }
        private void InitSections()
        {
            _graph = new NodeGraphSection(_ctx);
            _names = new(_ctx, _node.name);

            _info = new NodeInfoSection(serializedObject, _ctx);
            _requirements = new NodeRequirementsSection(serializedObject);
        }
        #endregion
        private void UpdateTime()
        {
            if (_lastUpdateTime == 0)
            {
                _lastUpdateTime = EditorApplication.timeSinceStartup;
                _ctx.UpdateTime(_lastUpdateTime);
            }
        }
    }
}