//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************

namespace Eiquif.UpgradeTree.Runtime
{
    public abstract class ContextSystem
    {
        protected NodeTree NodeTree { get; set; }
        protected double lastUpdateTime { get; set; }
        public double LastUpdateTime => lastUpdateTime;
        public NodeTree Tree => NodeTree;
        public INodeIDMenu IDMenu { get; protected set; }
    }
}