//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class HeaderStatusBadgeElement : IElement<NodeTreeHeaderContext>
    {
        public void Execute(NodeTreeHeaderContext ctx)
        {
            if (ctx == null) return;

            var tree = ctx.TreeContext.Tree;
            var rect = ctx.Rect;

            var nodeCount = tree.Nodes?.Count(n => n != null) ?? 0;
            var idCount = tree.IDs?.Count ?? 0;

            var subtitleStyle = new GUIStyle(EditorStyles.miniLabel)
            {
                normal = { textColor = new Color(0.7f, 0.7f, 0.7f) }
            };

            GUI.Label(
                new Rect(rect.x + 50, rect.y + 26, rect.width - 120, 16),
                $"{nodeCount} nodes • {idCount} IDs",
                subtitleStyle
            );

            var isValid =
                nodeCount > 0 &&
                tree.Nodes.All(n => n == null || !string.IsNullOrEmpty(n.ID.Value));

            EditorDrawStatusBadge.Draw(
                new Rect(rect.xMax - 70, rect.y + 15, 60, 20),
                isValid ? "Valid" : "Issues",
                isValid ? EditorColors.SuccessColor : EditorColors.WarningColor
            );
        }
    }
}