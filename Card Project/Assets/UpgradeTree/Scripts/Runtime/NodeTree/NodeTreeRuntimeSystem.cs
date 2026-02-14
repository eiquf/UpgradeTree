//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
namespace Eiquif.UpgradeTree.Runtime
{
    public abstract class NodeTreeRuntimeSystem
    {
        protected NodeTree Tree;

        public NodeTreeRuntimeSystem(NodeTree tree) => Tree = tree;
        public abstract void Execute();
    }
}