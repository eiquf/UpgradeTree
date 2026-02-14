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

        public NodeContext(
            SerializedObject so,
            Node node)
        {
            IDMenu = new NodeIDMenu();

            SerializedObject = so;
            Node = node;
            NextProp = so.FindProperty(NodePropertiesNames.NextNodes);
            PrerequisiteProp = so.FindProperty(NodePropertiesNames.PrerequisiteNodes);
        }

        public void UpdateTime(double time) => lastUpdateTime = time;
    }
}