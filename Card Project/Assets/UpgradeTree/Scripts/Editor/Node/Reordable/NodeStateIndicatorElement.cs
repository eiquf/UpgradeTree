//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public class NodeStateIndicatorElement : IElement<NodeListElementContext>
    {
        public void Execute(NodeListElementContext ctx)
        {
            if (ctx == null) return;

            var indicatorColor = ctx.Node == null
                ? EditorColors.ErrorColor
                : ctx.HasId ? EditorColors.SuccessColor : EditorColors.WarningColor;

            EditorGUI.DrawRect(
                new Rect(ctx.Rect.x - 4, ctx.Rect.y - 2, 3, ctx.Rect.height + 4),
                indicatorColor
            );
        }
    }

}