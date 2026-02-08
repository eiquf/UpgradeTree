//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Editor;

namespace Eiquif.UpgradeTree.Runtime
{
    public abstract class ContextSystem
    {
        protected NodeTree NodeTree { get; set; }
        public NodeTree Tree => NodeTree;
        public INodeIDMenu IDMenu { get; protected set; }
    }
}