//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
namespace Eiquif.UpgradeTree.Runtime
{
    public interface INodeTreeEditorService
    {
        Node CreateNode(NodeTree tree);
        void RemoveNode(NodeTree tree, Node node);
        void RemoveAllNodes(NodeTree tree);
    }
}