using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SummarySection : Section
{
    private readonly EditorFlowerAnimation _anim = new();
    
    private bool _showSummarySection = true;

    public SummarySection(NodeTreeContext ctx) : base(ctx) { }

    public override void Draw()
    {
        CollapsibleSection.Draw("Summary", "📊", ref _showSummarySection, EditorColors.InfoColor, () =>
        {
            DrawSummary();
        });
    }

    private void DrawSummary()
    {
        if (ctx.Tree.Nodes == null || ctx.Tree.Nodes.Count == 0)
        {
            EditorDrawUtils.DrawEmptyState("📊", "No Data", "Add nodes to see statistics");
            return;
        }

        var groups = ctx.Tree.Nodes
            .Where(n => n != null)
            .GroupBy(n => string.IsNullOrEmpty(n.ID.Value) ? "[No ID]" : n.ID.Value)
            .OrderByDescending(g => g.Count())
            .ToList();

        EditorGUILayout.BeginHorizontal();

        var totalNodes = ctx.Tree.Nodes.Count(n => n != null);
        var totalIds = ctx.Tree.IDs?.Count ?? 0;
        var assignedNodes = ctx.Tree.Nodes.Count(n => n != null && !string.IsNullOrEmpty(n.ID.Value));

        EditorDrawUtils.DrawStatCard("Nodes", totalNodes.ToString(), EditorColors.SecondaryColor);
        EditorDrawUtils.DrawStatCard("IDs", totalIds.ToString(), EditorColors.PrimaryColor);
        EditorDrawUtils.DrawStatCard("Assigned", $"{assignedNodes}/{totalNodes}", assignedNodes == totalNodes ? EditorColors.SuccessColor : EditorColors.WarningColor);

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(8);

        EditorGUILayout.LabelField("ID Distribution", EditorStyles.boldLabel);
        GUILayout.Space(4);

        foreach (var g in groups)
            DrawSummaryRow(g.Key, g.ToList());
    }
    private void DrawSummaryRow(string idKey, List<Node> nodes)
    {
        var isNoId = idKey == "[No ID]";
        var hasDuplicates = nodes.Count > 1 && !isNoId;

        EditorGUILayout.BeginHorizontal();

        var statusColor = isNoId ? EditorColors.WarningColor : (hasDuplicates ? EditorColors.InfoColor : EditorColors.SuccessColor);
        var rect = EditorGUILayout.GetControlRect(GUILayout.Width(4), GUILayout.Height(20));
        EditorGUI.DrawRect(rect, statusColor);

        GUILayout.Space(8);

        var labelStyle = new GUIStyle(EditorStyles.label) { fontStyle = isNoId ? FontStyle.Italic : FontStyle.Normal };
        EditorGUILayout.LabelField(idKey, labelStyle, GUILayout.Width(120));

        EditorGUILayout.LabelField($"×{nodes.Count}", EditorStyles.miniLabel, GUILayout.Width(40));

        GUILayout.FlexibleSpace();

        if (hasDuplicates)
        {
            var buttonStyle = new GUIStyle(EditorStyles.miniButton) { fontSize = 9 };
            if (GUILayout.Button("Keep First", buttonStyle, GUILayout.Width(70)))
            {
                Undo.RecordObject(ctx.Tree, "Remove Duplicates");
                var toRemove = nodes.Skip(1).ToList();
                ctx.Tree.Nodes = ctx.Tree.Nodes.Where(n => n == null || !toRemove.Contains(n)).ToList();
                EditorUtility.SetDirty(ctx.Tree);

                var buttonRect = GUILayoutUtility.GetLastRect();
                _anim.Spawn(new Vector2(buttonRect.center.x, buttonRect.center.y), toRemove.Count * 3);
            }
        }

        EditorGUILayout.EndHorizontal();
    }
}
