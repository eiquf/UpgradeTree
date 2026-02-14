//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class HeaderBordersElement : IElement<NodeTreeHeaderContext>
    {
        public void Execute(NodeTreeHeaderContext ctx)
        {
            if (ctx == null) return;

            EditorDrawPrimitives.DrawBorder(ctx.Rect,
                EditorColors.PrimaryColor,
                2);
        }
    }
}