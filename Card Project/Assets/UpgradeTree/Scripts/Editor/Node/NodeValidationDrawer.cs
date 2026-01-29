using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class NodeValidationDrawer
{
    private readonly EditorFlowerAnimation _anim = new();

    public void Draw(List<Node> nodes, Object undoTarget)
    {
        if (nodes == null || nodes.Count == 0) return;

        var nullCount = nodes.Count(n => n == null);
        var noIdCount = nodes.Count(n => n != null && string.IsNullOrEmpty(n.ID.Value));

        if (nullCount == 0 && noIdCount == 0) return;

        GUILayout.Space(8);
        EditorGUILayout.BeginVertical(EditorStyleCache.CardStyle);

        DrawHeader();

        if (nullCount > 0)
            DrawItem($"{nullCount} empty slot(s)", EditorColors.ErrorColor);

        if (noIdCount > 0)
            DrawItem($"{noIdCount} node(s) without ID", EditorColors.WarningColor);

        GUILayout.Space(8);

        if (GUILayout.Button("🧹 Clean Empty Slots", GUILayout.Height(24)))
        {
            Undo.RecordObject(undoTarget, "Clean Null Nodes");
            var removed = nodes.RemoveAll(n => n == null);
            EditorUtility.SetDirty(undoTarget);

            if (removed > 0)
            {
                var rect = GUILayoutUtility.GetLastRect();
                _anim?.Spawn(rect.center, removed * 3);
            }
        }

        EditorGUILayout.EndVertical();
    }

    protected virtual void DrawHeader()
    {
        var rect = EditorGUILayout.GetControlRect(false, 24);
        EditorGUI.DrawRect(rect, EditorColors.WarningBgLight);

        var style = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 11
        };
        style.normal.textColor = EditorColors.WarningColor;

        GUI.Label(rect, "⚠️ Validation Issues", style);
    }

    protected virtual void DrawItem(string text, Color color)
    {
        EditorGUILayout.BeginHorizontal();

        var dot = EditorGUILayout.GetControlRect(GUILayout.Width(8), GUILayout.Height(16));
        EditorGUI.DrawRect(new Rect(dot.x, dot.y + 4, 8, 8), color);

        EditorGUILayout.LabelField(text);
        EditorGUILayout.EndHorizontal();
    }
}