//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
namespace Eiquif.UpgradeTree.Runtime
{
    public abstract class UpgradeTreeRuntimeSystem
    {
        protected NodeTree Tree;

        public UpgradeTreeRuntimeSystem(NodeTree tree) => Tree = tree;
        public abstract void Execute();
    }
}