using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ActionNodeGroupDrawer
{
    private readonly FoldoutState _foldouts;
    private readonly ActionExecutor _executor;

    public ActionNodeGroupDrawer(FoldoutState foldouts, ActionExecutor executor)
    {
        _foldouts = foldouts;
        _executor = executor;
    }

    public void Draw(string actionName, string groupKey, List<Node> nodes, Action<Node> action)
    {
        var idKey = $"{actionName}_{groupKey}";
        var isNoId = groupKey.StartsWith("⚠️");

        EditorGUILayout.BeginHorizontal();

        DrawIndicator(isNoId);

        _foldouts.Set(
            idKey,
            EditorGUILayout.Foldout(_foldouts.Get(idKey), groupKey, true)
        );

        DrawCount(nodes.Count);

        GUILayout.FlexibleSpace();

        if (!isNoId && GUILayout.Button("▶ Run All", EditorStyles.miniButton, GUILayout.Width(70)))
            _executor.Run(action, nodes);

        EditorGUILayout.EndHorizontal();

        if (_foldouts.Get(idKey))
            DrawNodes(nodes, action);
    }

    private void DrawIndicator(bool isNoId)
    {
        var rect = EditorGUILayout.GetControlRect(GUILayout.Width(4), GUILayout.Height(18));
        EditorGUI.DrawRect(rect, isNoId ? EditorColors.WarningColor : EditorColors.SuccessColor);
        GUILayout.Space(4);
    }

    private void DrawCount(int count)
    {
        var style = new GUIStyle(EditorStyles.miniLabel) { normal = { textColor = Color.grey } };
        GUILayout.Label($"({count})", style, GUILayout.Width(30));
    }

    private void DrawNodes(List<Node> nodes, Action<Node> action)
    {
        EditorGUI.indentLevel++;
        foreach (var node in nodes)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(node, typeof(Node), false);

            if (GUILayout.Button("▶", GUILayout.Width(24)))
                _executor.Run(action, node);

            EditorGUILayout.EndHorizontal();
        }
        EditorGUI.indentLevel--;
    }
}