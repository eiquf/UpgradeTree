//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class NodeHeaderBackgroundElement : IElement<NodeHeaderContext>
    {
        public void Execute(NodeHeaderContext ctx)
        {
            if (ctx == null) return;

            EditorDrawPrimitives.DrawGradientRect(
                ctx.Rect,
                new Color(0.2f, 0.25f, 0.35f),
                new Color(0.15f, 0.18f, 0.25f)
            );
        }
    }
}