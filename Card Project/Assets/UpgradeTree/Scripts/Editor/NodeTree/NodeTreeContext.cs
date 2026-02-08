//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Editor;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Runtime
{
    public class NodeTreeContext : ContextSystem
    {
        public SerializedObject SerializedObject { get; }
        public SerializedProperty NodesProp { get; }
        public SerializedProperty IDsProp { get; }

        public double LastUpdateTime { get; private set; }

        public NodeTreeContext(
            NodeTree tree,
            SerializedObject so)
        {
            NodeTree = tree;
            SerializedObject = so;

            NodesProp = so.FindProperty(NodeTreePropertiesNames.Nodes);
            IDsProp = so.FindProperty(NodeTreePropertiesNames.IDs);

            IDMenu = new NodeIDMenu();

            Errors();
        }

        public void Record(string label)
        {
            Undo.RecordObject(Tree, label);
            UnityEditor.EditorUtility.SetDirty(Tree);
        }
#if UNITY_EDITOR
        #region Future addition
        public void UpdateTime(double time) => LastUpdateTime = time;
        #endregion
#endif
        private void Errors()
        {
            if (IDsProp == null)
                Debug.LogError("[NodeTreeEditor] IDsProp is null!");

            if (NodesProp == null)
                Debug.LogError("[NodeTreeEditor] NodesProp is null!");
        }
    }
}