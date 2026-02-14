//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public class NodeBackgroundElement : IElement<NodeListElementContext>
    {
        public void Execute(NodeListElementContext ctx)
        {
            if (ctx == null) return;

            var bgColor = ctx.Node == null
                ? EditorColors.ErrorBgLight
                : ctx.HasId ? EditorColors.SuccessBgLight : EditorColors.WarningBgLight;

            EditorGUI.DrawRect(
                new Rect(ctx.Rect.x - 1, ctx.Rect.y - 2, ctx.Rect.width + 2, ctx.Rect.height + 4),
                bgColor
            );
        }
    }
}