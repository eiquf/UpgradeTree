using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class NodeHeaderTitleElement : IElement<NodeHeaderContext>
    {
        public void Execute(NodeHeaderContext ctx)
        {
            if (ctx == null) return;

            var style = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 14,
                normal = { textColor = Color.white }
            };

            GUI.Label(
                new Rect(ctx.Rect.x + 50, ctx.Rect.y + 8, ctx.Rect.width - 120, 20),
                ctx.Name,
                style
            );
        }
    }
}