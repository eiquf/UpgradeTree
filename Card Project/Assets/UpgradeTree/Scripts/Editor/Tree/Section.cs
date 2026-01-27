public abstract class Section
{
    protected NodeTreeContext ctx;

    public Section(NodeTreeContext ctx)
    {
        this.ctx = ctx;
    }

    public abstract void Draw();
}