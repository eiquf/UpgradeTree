//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public static class EditorBadges
    {
        public static void DrawStatusBadge(
            Rect rect,
            string text,
            Color color)
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

        public static void DrawCountBadge(
            Rect rect,
            int count,
            Color color)
        {
            var badgeRect = new Rect(
                rect.x,
                rect.y + 2,
                rect.width,
                rect.height - 4);

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

        public static void DrawMiniStatBadge(
            string text,
            Color color)
        {
            var content = new GUIContent(text);
            var size = EditorStyles.miniLabel.CalcSize(content);
            var rect = EditorGUILayout.GetControlRect(
                GUILayout.Width(size.x + 12),
                GUILayout.Height(18));

            EditorGUI.DrawRect(
                rect,
                new Color(color.r, color.g, color.b, 0.2f));

            var style = new GUIStyle(EditorStyles.miniLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 10
            };
            style.normal.textColor = color;

            GUI.Label(rect, text, style);
        }
    }
}