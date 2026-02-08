using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class SummaryDistributionRowsElement : IElement<SummaryCtx>
    {
        public void Execute(SummaryCtx ctx)
        {
            if (ctx == null) return;

            foreach (var g in ctx.Groups)
                DrawRow(ctx, g);
        }

        private void DrawRow(SummaryCtx ctx, SummaryGroup g)
        {
            EditorGUILayout.BeginHorizontal();

            var color = g.IsNoId
                ? EditorColors.WarningColor
                : g.HasDuplicates
                    ? EditorColors.InfoColor
                    : EditorColors.SuccessColor;

            var rect = EditorGUILayout.GetControlRect(GUILayout.Width(4), GUILayout.Height(20));
            EditorGUI.DrawRect(rect, color);

            GUILayout.Space(8);

            var labelStyle = new GUIStyle(EditorStyles.label)
            {
                fontStyle = g.IsNoId ? FontStyle.Italic : FontStyle.Normal
            };

            EditorGUILayout.LabelField(g.IdKey, labelStyle, GUILayout.Width(120));
            EditorGUILayout.LabelField($"×{g.Nodes.Count}", EditorStyles.miniLabel, GUILayout.Width(40));

            GUILayout.FlexibleSpace();

            if (g.HasDuplicates && GUILayout.Button("Keep First", EditorStyles.miniButton, GUILayout.Width(70)))
            {
                Undo.RecordObject(ctx.TreeContext.Tree, "Remove Duplicates");

                var toRemove = g.Nodes.Skip(1).ToList();
                ctx.TreeContext.Tree.Nodes =
                    ctx.TreeContext.Tree.Nodes
                        .Where(n => n == null || !toRemove.Contains(n))
                        .ToList();

                EditorUtility.SetDirty(ctx.TreeContext.Tree);

                var btnRect = GUILayoutUtility.GetLastRect();
                ctx.Anim.Spawn(btnRect.center, toRemove.Count * 3);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}