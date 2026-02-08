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

            EditorDrawPrimitives.DrawGradientRect(
                ctx.Rect,
                EditorColors.PrimaryColor,
                EditorColors.PrimaryColor,
                2
            );
        }
    }
}