//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;


namespace Eiquif.UpgradeTree.Editor
{
    public static class EditorDrawStatusBadge
    {
        public static void Draw(
                Rect rect,
                string text,
                Color background,
                Color? textColor = null,
                int fontSize = 9)
        {
            EditorGUI.DrawRect(rect, background);

            var style = new GUIStyle(EditorStyles.miniLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = fontSize,
                fontStyle = FontStyle.Bold
            };

            style.normal.textColor = textColor ?? Color.white;

            GUI.Label(rect, text, style);
        }
    }
}