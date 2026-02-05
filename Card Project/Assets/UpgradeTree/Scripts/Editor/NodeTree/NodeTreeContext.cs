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
            SerializedObject so,
            SerializedProperty nodes,
            SerializedProperty ids)
        {
            NodeTree = tree;
            SerializedObject = so;
            NodesProp = nodes;
            IDsProp = ids;

            IDMenu = new NodeIDMenu();

            if (IDsProp == null)
                Debug.LogError("[NodeTreeEditor] IDsProp is null!");

            if (NodesProp == null)
                Debug.LogError("[NodeTreeEditor] NodesProp is null!");
        }

        public void Record(string label)
        {
            Undo.RecordObject(Tree, label);
            EditorUtility.SetDirty(Tree);
        }

        public void UpdateTime(double time) => LastUpdateTime = time;
    }
}