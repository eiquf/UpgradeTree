//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class NodeGraphAnimationElement : IElement<NodeContext>
    {
        private readonly EditorFlowerAnimation _anim = new();

        public void Execute(NodeContext ctx)
        {
            if (ctx == null) return;
            _anim.UpdateAndDrawFlowers(ctx.LastUpdateTime);
        }
    }
}