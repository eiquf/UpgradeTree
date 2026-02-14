//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class HeaderTitleElement : IElement<NodeTreeHeaderContext>
    {
        public void Execute(NodeTreeHeaderContext ctx)
        {
            if (ctx == null) return;

            var style = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 14,
                normal = { textColor = Color.white }
            };

            var rect = new Rect(
                ctx.Rect.x + 50,
                ctx.Rect.y + 8,
                ctx.Rect.width - 120,
                20
            );

            GUI.Label(rect, ctx.Name, style);
        }
    }
}