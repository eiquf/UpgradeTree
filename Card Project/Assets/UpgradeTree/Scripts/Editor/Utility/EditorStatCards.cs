//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public static class EditorStatCards
    {
        private const float CARD_WIDTH = 70f;

        public static void DrawStatCard(
            string label,
            string value,
            Color accentColor)
        {
            EditorGUILayout.BeginVertical(
                EditorStyleCache.CardStyle,
                GUILayout.Width(CARD_WIDTH));

            var accentRect =
                EditorGUILayout.GetControlRect(false, 3, GUILayout.Width(CARD_WIDTH));
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
                fontSize = 9,
                alignment = TextAnchor.MiddleCenter
            };
            EditorGUILayout.LabelField(label, labelStyle);

            EditorGUILayout.EndVertical();
        }
    }
}
