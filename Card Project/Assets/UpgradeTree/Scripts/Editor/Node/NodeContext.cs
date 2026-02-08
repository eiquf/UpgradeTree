//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Editor;
using UnityEditor;

namespace Eiquif.UpgradeTree.Runtime
{
    public class NodeContext : ContextSystem
    {
        public SerializedObject SerializedObject { get; }
        public Node Node { get; }
        public SerializedProperty NextProp { get; }
        public SerializedProperty PrerequisiteProp { get; }

        public double LastUpdateTime { get; private set; }

        public NodeContext(
            SerializedObject so,
            Node node,
            SerializedProperty next,
            SerializedProperty prerequisite)
        {

            IDMenu = new NodeIDMenu();

            SerializedObject = so;
            Node = node;
            NextProp = next;
            PrerequisiteProp = prerequisite;
        }

        public void UpdateTime(double time) => LastUpdateTime = time;
    }
}