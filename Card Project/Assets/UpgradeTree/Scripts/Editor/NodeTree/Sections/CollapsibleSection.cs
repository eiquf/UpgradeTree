using System;
using UnityEditor;
using UnityEngine;

public static class CollapsibleSection
{
    public static void Draw(
        string title,
        string icon,
        ref bool isExpanded,
        Color accentColor,
        Action drawContent,
        bool showArrow = true)
    {
        var containerRect = EditorGUILayout.BeginVertical(EditorStyleCache.CardStyle);

        var headerRect = EditorGUILayout.GetControlRect(false, 28);

        EditorGUI.DrawRect(headerRect, EditorColors.SectionHeaderBg);

        var accentRect = new Rect(headerRect.x, headerRect.y, 4, headerRect.height);
        EditorGUI.DrawRect(accentRect, accentColor);

        if (showArrow)
        {
            var arrowRect = new Rect(headerRect.x + 10, headerRect.y, 20, headerRect.height);
            var arrow = isExpanded ? "▼" : "▶";
            var arrowStyle = new GUIStyle(EditorStyles.label) { fontSize = 10, alignment = TextAnchor.MiddleCenter };
            arrowStyle.normal.textColor = Color.grey;
            GUI.Label(arrowRect, arrow, arrowStyle);
        }

        var iconRect = new Rect(headerRect.x + 28, headerRect.y, 24, headerRect.height);
        var iconStyle = new GUIStyle { fontSize = 14, alignment = TextAnchor.MiddleCenter };
        GUI.Label(iconRect, icon, iconStyle);

        var titleRect = new Rect(headerRect.x + 52, headerRect.y, headerRect.width - 60, headerRect.height);
        var titleStyle = new GUIStyle(EditorStyles.boldLabel) { fontSize = 12, alignment = TextAnchor.MiddleLeft };
        titleStyle.normal.textColor = new Color(0.9f, 0.9f, 0.9f);
        GUI.Label(titleRect, title, titleStyle);

        if (GUI.Button(headerRect, GUIContent.none, GUIStyle.none))
            isExpanded = !isExpanded;

        if (isExpanded)
        {
            GUILayout.Space(8);
            drawContent?.Invoke();
            GUILayout.Space(4);
        }

        EditorGUILayout.EndVertical();
    }

    public static void DrawProperty(string title,
        string icon,
        ref bool isExpanded,
        Color accentColor,
        Action drawContent)
    {
        var containerRect = EditorGUILayout.BeginVertical(EditorStyleCache.CardStyle);

        var headerRect = EditorGUILayout.GetControlRect(false, 28);

        EditorGUI.DrawRect(headerRect, EditorColors.SectionHeaderBg);

        var accentRect = new Rect(headerRect.x, headerRect.y, 4, headerRect.height);
        EditorGUI.DrawRect(accentRect, accentColor);

        var iconRect = new Rect(headerRect.x + 4, headerRect.y, 24, headerRect.height);
        var iconStyle = new GUIStyle { fontSize = 14, alignment = TextAnchor.MiddleCenter };
        GUI.Label(iconRect, icon, iconStyle);

        var titleRect = new Rect(headerRect.x + 28, headerRect.y, headerRect.width - 60, headerRect.height);
        var titleStyle = new GUIStyle(EditorStyles.boldLabel) { fontSize = 12, alignment = TextAnchor.MiddleLeft };
        titleStyle.normal.textColor = new Color(0.9f, 0.9f, 0.9f);
        GUI.Label(titleRect, title, titleStyle);

        if (GUI.Button(headerRect, GUIContent.none, GUIStyle.none))
            isExpanded = !isExpanded;

        if (isExpanded)
        {
            GUILayout.Space(8);
            drawContent?.Invoke();
            GUILayout.Space(4);
        }

        EditorGUILayout.EndVertical();
    }
}
