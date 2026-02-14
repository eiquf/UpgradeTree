//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class NodeHeaderStatusBadgeElement : IElement<NodeHeaderContext>
    {
        public void Execute(NodeHeaderContext ctx)
        {
            if (ctx == null) return;

            var node = ctx.NodeContext.Node;
            var rect = ctx.Rect;

            var nextCount = node.NextNodes?.Count(n => n != null) ?? 0;
            var prereqCount = node.PrerequisiteNodes?.Count ?? 0;

            var subtitleStyle = new GUIStyle(EditorStyles.miniLabel)
            {
                normal = { textColor = new Color(0.7f, 0.7f, 0.7f) }
            };

            GUI.Label(
                new Rect(rect.x + 50, rect.y + 26, rect.width - 120, 16),
                $"{nextCount} next nodes • {prereqCount} prerequisite nodes",
                subtitleStyle
            );

            var isValid =
                nextCount > 0 &&
                node.NextNodes.All(n => n == null || !string.IsNullOrEmpty(n.ID.Value));

            EditorBadges.DrawStatusBadge(
                new Rect(rect.xMax - 70, rect.y + 15, 60, 20),
                isValid ? "Valid" : "Issues",
                isValid ? EditorColors.SuccessColor : EditorColors.WarningColor
            );
        }
    }
}