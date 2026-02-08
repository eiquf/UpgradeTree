//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
namespace Eiquif.UpgradeTree.Editor
{
    public sealed class NodeHeaderBordersElement : IElement<NodeHeaderContext>
    {
        public void Execute(NodeHeaderContext ctx)
        {
            if (ctx == null) return;
            EditorDrawPrimitives.DrawBorder(ctx.Rect, EditorColors.PrimaryColor, 2);
        }
    }
}