using UnityEditor;
using UnityEngine;

public static class EditorDrawUtils
{
    public static void DrawGradientRect(Rect rect, Color top, Color bottom)
    {
        // Number of gradient steps
        var steps = 10;
        var stepHeight = rect.height / steps;

        // Loop through and draw each gradient step
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
        // Top border
        EditorGUI.DrawRect(new Rect(rect.x, rect.y, rect.width, thickness), color);
        // Bottom border
        EditorGUI.DrawRect(new Rect(rect.x, rect.yMax - thickness, rect.width, thickness), color);
        // Left border
        EditorGUI.DrawRect(new Rect(rect.x, rect.y, thickness, rect.height), color);
        // Right border
        EditorGUI.DrawRect(new Rect(rect.xMax - thickness, rect.y, thickness, rect.height), color);
    }

    /// <param name="color">The background color of the badge.</param>
    public static void DrawStatusBadge(Rect rect, string text, Color color)
    {
        // Draw the badge background
        EditorGUI.DrawRect(rect, color);

        // Set up the label style for the badge text
        var style = new GUIStyle(EditorStyles.miniLabel)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 9,
            fontStyle = FontStyle.Bold
        };
        style.normal.textColor = Color.white;

        // Draw the badge text
        GUI.Label(rect, text, style);
    }

    public static void DrawCountBadge(Rect rect, int count, Color color)
    {
        // Define the badge rect with some padding
        var badgeRect = new Rect(rect.x, rect.y + 2, rect.width, rect.height - 4);

        // Draw the badge background
        EditorGUI.DrawRect(badgeRect, color);

        // Set up the label style for the count text
        var style = new GUIStyle(EditorStyles.miniLabel)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 10,
            fontStyle = FontStyle.Bold
        };
        style.normal.textColor = Color.white;

        // Draw the count number
        GUI.Label(badgeRect, count.ToString(), style);
    }


    public static void DrawMiniStatBadge(string text, Color color)
    {
        // Create content from the given text and calculate the required size
        var content = new GUIContent(text);
        var size = EditorStyles.miniLabel.CalcSize(content);
        var rect = EditorGUILayout.GetControlRect(GUILayout.Width(size.x + 12), GUILayout.Height(18));

        // Draw a semi-transparent background for the badge
        EditorGUI.DrawRect(rect, new Color(color.r, color.g, color.b, 0.2f));

        // Set up the label style for the stat text
        var style = new GUIStyle(EditorStyles.miniLabel)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 10
        };
        style.normal.textColor = color;

        // Draw the text inside the badge
        GUI.Label(rect, text, style);
    }
    public static void DrawStatCard(string label, string value, Color accentColor)
    {
        const float CARD_WIDTH = 70f;

        EditorGUILayout.BeginVertical(
            EditorStyleCache.CardStyle,
            GUILayout.Width(CARD_WIDTH),
            GUILayout.MaxWidth(CARD_WIDTH)
        );

        var accentRect = EditorGUILayout.GetControlRect(false, 3, GUILayout.Width(CARD_WIDTH));
        EditorGUI.DrawRect(accentRect, accentColor);

        GUILayout.Space(4);

        var valueStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 16,
            alignment = TextAnchor.MiddleCenter
        };
        EditorGUILayout.LabelField(value, valueStyle, GUILayout.Width(CARD_WIDTH));

        var labelStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
        {
            fontSize = 9,
            alignment = TextAnchor.MiddleCenter
        };
        EditorGUILayout.LabelField(label, labelStyle, GUILayout.Width(CARD_WIDTH));

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

public sealed class EditorGradientRect : IEditorElement
{
    #region Colors
    private readonly Color _top;
    private readonly Color _bottom;
    #endregion

    private readonly int _steps = 10;
    public EditorGradientRect(Color top, Color bottom, int steps = 10)
    {
        _top = top;
        _bottom = bottom;
        _steps = steps;
    }
    public void Draw(Rect rect)
    {
        var stepHeight = rect.height / _steps;
        for (var i = 0; i < _steps; i++)
        {
            var t = (float)i / _steps;
            var color = Color.Lerp(_top, _bottom, t);
            var stepRect = new Rect(rect.x, rect.y + i * stepHeight, rect.width, stepHeight + 1);
            EditorGUI.DrawRect(stepRect, color);
        }
    }
}

public sealed class EditorDrawBorders : IEditorElement
{
    private readonly float _thickness;
    private readonly Color _color;
    public EditorDrawBorders(Color color, float thickness)
    {
        _thickness = thickness;
        _color = color;
    }
    public void Draw(Rect rect)
    {
        EditorGUI.DrawRect(new Rect(rect.x, rect.y, rect.width, _thickness), _color);
        EditorGUI.DrawRect(new Rect(rect.x, rect.yMax - _thickness, rect.width, _thickness), _color);
        EditorGUI.DrawRect(new Rect(rect.x, rect.y, _thickness, rect.height), _color);
        EditorGUI.DrawRect(new Rect(rect.xMax - _thickness, rect.y, _thickness, rect.height), _color);
    }
}