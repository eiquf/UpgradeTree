//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class ProgressionHeaderBordersElement : IElement<ProgressionHeaderContext>
    {
        public void Execute(ProgressionHeaderContext ctx)
        {
            if (ctx == null) return;
            EditorDrawPrimitives.DrawBorder(ctx.Rect, EditorColors.PrimaryColor, 2);
        }
    }
}