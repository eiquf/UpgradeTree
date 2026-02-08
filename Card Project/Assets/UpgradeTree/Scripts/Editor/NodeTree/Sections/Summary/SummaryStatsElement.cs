using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class SummaryStatsElement : IElement<SummaryCtx>
    {
        public void Execute(SummaryCtx? ctx)
        {
            if (ctx == null) return;

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            EditorStatCards.DrawStatCard(
                "Nodes",
                ctx.TotalNodes.ToString(),
                EditorColors.SecondaryColor
            );

            EditorStatCards.DrawStatCard(
                "IDs",
                ctx.TotalIds.ToString(),
                EditorColors.PrimaryColor
            );

            EditorStatCards.DrawStatCard(
                "Assigned",
                $"{ctx.AssignedNodes}/{ctx.TotalNodes}",
                ctx.AssignedNodes == ctx.TotalNodes
                    ? EditorColors.SuccessColor
                    : EditorColors.WarningColor
            );

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
    }

}