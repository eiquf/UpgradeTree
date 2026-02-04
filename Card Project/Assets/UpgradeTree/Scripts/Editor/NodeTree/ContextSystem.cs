namespace Eiquif.UpgradeTree.Runtime.Node
{
    using Eiquif.UpgradeTree.Editor;
    using Eiquif.UpgradeTree.Runtime.Tree;

    public abstract class ContextSystem
    {
        protected NodeTree NodeTree { get; set; }
        public NodeTree Tree => NodeTree;
        public INodeIDMenu IDMenu { get; protected set; }
    }
}