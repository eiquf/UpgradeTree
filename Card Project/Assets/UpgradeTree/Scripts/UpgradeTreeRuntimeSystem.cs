public abstract class UpgradeTreeRuntimeSystem
{
    protected NodeTree Tree;

    public UpgradeTreeRuntimeSystem(NodeTree tree) => Tree = tree;
    public abstract void Execute();
}