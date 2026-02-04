using Eiquif.UpgradeTree.Runtime.Node;

public abstract class Section
{
    protected ContextSystem ctx;

    public Section(ContextSystem ctx)
    {
        this.ctx = ctx;
    }

    public abstract void Draw();
}