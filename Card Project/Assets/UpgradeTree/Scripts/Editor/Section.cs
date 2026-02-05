//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
// Description: Abstract section for all section types setup and create
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;

public abstract class Section
{
    protected ContextSystem ctx;

    public Section(ContextSystem ctx)
    {
        this.ctx = ctx;
    }

    public abstract void Draw();
}