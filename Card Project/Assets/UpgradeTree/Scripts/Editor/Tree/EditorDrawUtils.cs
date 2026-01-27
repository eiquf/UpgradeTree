using UnityEditor;
using UnityEngine;

public static class EditorDrawUtils
{
    public static void DrawGradientRect(Rect rect, Color top, Color bottom)
    {
        var steps = 10;
        var stepHeight = rect.height / steps;

        for (var i = 0; i < steps; i++)
        {
            var t = (float)i / steps;
            var color = Color.Lerp(top, bottom, t);
            var stepRect = new Rect(rect.x, rect.y + i * stepHeight, rect.width, stepHeight + 1);
            EditorGUI.DrawRect(stepRect, color);
        }
    }

    public static void DrawBorder(Rect rect, Color color, float thickness = 1)
    {
        EditorGUI.DrawRect(new Rect(rect.x, rect.y, rect.width, thickness), color);
        EditorGUI.DrawRect(new Rect(rect.x, rect.yMax - thickness, rect.width, thickness), color);
        EditorGUI.DrawRect(new Rect(rect.x, rect.y, thickness, rect.height), color);
        EditorGUI.DrawRect(new Rect(rect.xMax - thickness, rect.y, thickness, rect.height), color);
    }

    public static void DrawStatusBadge(Rect rect, string text, Color color)
    {
        EditorGUI.DrawRect(rect, color);

        var style = new GUIStyle(EditorStyles.miniLabel)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 9,
            fontStyle = FontStyle.Bold
        };
        style.normal.textColor = Color.white;

        GUI.Label(rect, text, style);
    }

    public static void DrawCountBadge(Rect rect, int count, Color color)
    {
        var badgeRect = new Rect(rect.x, rect.y + 2, rect.width, rect.height - 4);

        EditorGUI.DrawRect(badgeRect, color);

        var style = new GUIStyle(EditorStyles.miniLabel)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 10,
            fontStyle = FontStyle.Bold
        };
        style.normal.textColor = Color.white;

        GUI.Label(badgeRect, count.ToString(), style);
    }

    public static void DrawMiniStatBadge(string text, Color color)
    {
        var content = new GUIContent(text);
        var size = EditorStyles.miniLabel.CalcSize(content);
        var rect = EditorGUILayout.GetControlRect(GUILayout.Width(size.x + 12), GUILayout.Height(18));

        EditorGUI.DrawRect(rect, new Color(color.r, color.g, color.b, 0.2f));

        var style = new GUIStyle(EditorStyles.miniLabel)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 10
        };
        style.normal.textColor = color;

        GUI.Label(rect, text, style);
    }

    public static void DrawStatCard(string label, string value, Color accentColor)
    {
        EditorGUILayout.BeginVertical(EditorStyleCache.CardStyle, GUILayout.MinWidth(70));

        var accentRect = EditorGUILayout.GetControlRect(false, 3);
        EditorGUI.DrawRect(accentRect, accentColor);

        GUILayout.Space(4);

        var valueStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 16,
            alignment = TextAnchor.MiddleCenter
        };
        EditorGUILayout.LabelField(value, valueStyle);

        var labelStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
        {
            fontSize = 9
        };
        EditorGUILayout.LabelField(label, labelStyle);

        EditorGUILayout.EndVertical();
    }

    public static void DrawEmptyState(string icon, string title, string subtitle)
    {
        GUILayout.Space(16);

        EditorGUILayout.BeginVertical();

        var iconStyle = new GUIStyle { fontSize = 32, alignment = TextAnchor.MiddleCenter };
        GUILayout.Label(icon, iconStyle, GUILayout.Height(40));

        var titleStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 12,
            alignment = TextAnchor.MiddleCenter
        };
        titleStyle.normal.textColor = new Color(0.7f, 0.7f, 0.7f);
        GUILayout.Label(title, titleStyle);

        var subtitleStyle = new GUIStyle(EditorStyles.miniLabel)
        {
            alignment = TextAnchor.MiddleCenter
        };
        subtitleStyle.normal.textColor = new Color(0.5f, 0.5f, 0.5f);
        GUILayout.Label(subtitle, subtitleStyle);

        EditorGUILayout.EndVertical();

        GUILayout.Space(16);
    }
}