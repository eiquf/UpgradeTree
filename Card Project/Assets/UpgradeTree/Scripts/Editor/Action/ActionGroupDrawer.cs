using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ActionGroupDrawer
{
    private readonly FoldoutState _foldouts;
    private readonly ActionNodeGroupDrawer _nodeDrawer;

    public ActionGroupDrawer(
        FoldoutState foldouts,
        ActionNodeGroupDrawer nodeDrawer)
    {
        _foldouts = foldouts;
        _nodeDrawer = nodeDrawer;
    }

    public void Draw(
        string actionName,
        List<Action<Node>> actions,
        List<Node> nodes,
        Action onDelete)
    {
        EditorGUILayout.BeginVertical(EditorStyleCache.CardStyle);
        EditorGUILayout.BeginHorizontal();

        DrawHeader(actionName, actions.Count, onDelete);

        EditorGUILayout.EndHorizontal();

        if (_foldouts.Get(actionName))
            DrawNodes(actionName, actions.First(), nodes);

        EditorGUILayout.EndVertical();
    }

    private void DrawHeader(string actionName, int count, Action onDelete)
    {
        var arrow = _foldouts.Get(actionName) ? "▼" : "▶";
        if (GUILayout.Button(arrow, EditorStyles.label, GUILayout.Width(20)))
            _foldouts.Toggle(actionName);

        EditorGUILayout.LabelField($"🎯 {actionName}", EditorStyles.boldLabel);

        EditorDrawUtils.DrawCountBadge(
            EditorGUILayout.GetControlRect(GUILayout.Width(30), GUILayout.Height(18)),
            count,
            EditorColors.AccentColor
        );

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("✕", EditorStyles.miniButton, GUILayout.Width(24)))
            onDelete?.Invoke();
    }

    private void DrawNodes(string actionName, Action<Node> action, List<Node> nodes)
    {
        var grouped = nodes
            .Where(n => n != null)
            .GroupBy(n => string.IsNullOrEmpty(n.ID.Value) ? "⚠️ [No ID]" : n.ID.Value)
            .OrderBy(g => g.Key.StartsWith("⚠️") ? 1 : 0);

        foreach (var group in grouped)
            _nodeDrawer.Draw(actionName, group.Key, group.ToList(), action);
    }
}