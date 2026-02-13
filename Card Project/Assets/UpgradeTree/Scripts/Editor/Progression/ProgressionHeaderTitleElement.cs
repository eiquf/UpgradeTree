//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class ProgressionHeaderTitleElement : IElement<ProgressionHeaderContext>
    {
        public void Execute(ProgressionHeaderContext ctx)
        {
            if (ctx == null) return;

            var style = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 14,
                normal = { textColor = Color.white }
            };

            GUI.Label(
                new Rect(ctx.Rect.x + 50, ctx.Rect.y + 10, ctx.Rect.width - 100, 20),
                ctx.Name,
                style
            );
        }
    }
}