using UnityEditor;
using UnityEngine;

/// <summary>
/// A utility class that contains methods for drawing various custom editor UI elements in the Unity Editor.
/// These methods include drawing gradient rectangles, borders, badges, stat cards, and empty state screens.
/// </summary>
public static class EditorDrawUtils
{
    /// <summary>
    /// Draws a vertical gradient rectangle within a given <see cref="Rect"/>.
    /// The gradient transitions from the specified top color to the bottom color.
    /// </summary>
    /// <param name="rect">The rectangle where the gradient should be drawn.</param>
    /// <param name="top">The top color of the gradient.</param>
    /// <param name="bottom">The bottom color of the gradient.</param>
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

    /// <summary>
    /// Draws a border around the given <see cref="Rect"/> with the specified color and thickness.
    /// The border is drawn on all four sides.
    /// </summary>
    /// <param name="rect">The rectangle where the border should be drawn.</param>
    /// <param name="color">The color of the border.</param>
    /// <param name="thickness">The thickness of the border. Default is 1.</param>
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

    /// <summary>
    /// Draws a status badge inside the given rectangle with the specified text and background color.
    /// The badge is typically used for status indicators.
    /// </summary>
    /// <param name="rect">The rectangle where the status badge should be drawn.</param>
    /// <param name="text">The text to display inside the badge.</param>
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

    /// <summary>
    /// Draws a count badge that displays a numerical value inside a given rectangle.
    /// The badge is typically used to display counts or numbers (e.g., item counts, notifications).
    /// </summary>
    /// <param name="rect">The rectangle where the count badge should be drawn.</param>
    /// <param name="count">The numerical value to display inside the badge.</param>
    /// <param name="color">The background color of the badge.</param>
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

    /// <summary>
    /// Draws a mini stat badge with the provided text and accent color.
    /// This is typically used for small stats or labels.
    /// </summary>
    /// <param name="text">The text to display inside the stat badge.</param>
    /// <param name="color">The accent color used for the badge background and text.</param>
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

    /// <summary>
    /// Draws a stat card with a label and value, using the specified accent color for visual emphasis.
    /// This is useful for displaying key statistics or information in a card-like format.
    /// </summary>
    /// <param name="label">The label (title) to display on the card.</param>
    /// <param name="value">The value (e.g., number or text) to display inside the card.</param>
    /// <param name="accentColor">The accent color used for visual emphasis.</param>
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


    /// <summary>
    /// Draws an empty state UI element with an icon, title, and subtitle. Typically used to display
    /// an "empty" state when there are no items or data available.
    /// </summary>
    /// <param name="icon">The icon to display at the top of the empty state UI (typically a string or icon image).</param>
    /// <param name="title">The title text to describe the empty state (e.g., "No Items Available").</param>
    /// <param name="subtitle">The subtitle text that provides additional context or description of the empty state.</param>
    public static void DrawEmptyState(string icon, string title, string subtitle)
    {
        GUILayout.Space(16);

        // Begin drawing the empty state container
        EditorGUILayout.BeginVertical();

        // Set up the style for the icon
        var iconStyle = new GUIStyle { fontSize = 32, alignment = TextAnchor.MiddleCenter };
        GUILayout.Label(icon, iconStyle, GUILayout.Height(40));

        // Set up the style for the title text
        var titleStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 12,
            alignment = TextAnchor.MiddleCenter
        };
        titleStyle.normal.textColor = new Color(0.7f, 0.7f, 0.7f);
        GUILayout.Label(title, titleStyle);

        // Set up the style for the subtitle text
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
