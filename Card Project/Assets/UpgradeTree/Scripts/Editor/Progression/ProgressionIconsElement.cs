//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class ProgressionIconsElement : IElement<ProgressionHeaderContext>
    {
        public void Execute(ProgressionHeaderContext ctx)
        {
            if (ctx == null) return;

            var rect = ctx.Rect;
            var flowerStyle = new GUIStyle
            {
                fontSize = 16,
                alignment = TextAnchor.MiddleCenter
            };

            GUI.Label(new Rect(rect.x + 4, rect.y + 2, 20, 20), "🍌", flowerStyle);
            GUI.Label(new Rect(rect.xMax - 24, rect.y + 2, 20, 20), "🌺", flowerStyle);
            GUI.Label(new Rect(rect.x + 4, rect.yMax - 22, 20, 20), "🌷", flowerStyle);
            GUI.Label(new Rect(rect.xMax - 24, rect.yMax - 22, 20, 20), "🌻", flowerStyle);

            var iconRect = new Rect(rect.x + 12, rect.y + 10, 30, 30);
            GUI.Label(iconRect, "🌠", new GUIStyle
            {
                fontSize = 24,
                alignment = TextAnchor.MiddleCenter
            });
        }
    }
}