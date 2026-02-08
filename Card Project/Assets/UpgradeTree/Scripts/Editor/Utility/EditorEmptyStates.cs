//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public static class EditorEmptyStates
    {
        public static void Draw(
            string icon,
            string title,
            string subtitle)
        {
            GUILayout.Space(16);
            EditorGUILayout.BeginVertical();

            GUILayout.Label(
                icon,
                new GUIStyle
                {
                    fontSize = 32,
                    alignment = TextAnchor.MiddleCenter
                },
                GUILayout.Height(40));

            GUILayout.Label(
                title,
                new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 12,
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = new Color(.7f, .7f, .7f) }
                });

            GUILayout.Label(
                subtitle,
                new GUIStyle(EditorStyles.miniLabel)
                {
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = new Color(.5f, .5f, .5f) }
                });

            EditorGUILayout.EndVertical();
            GUILayout.Space(16);
        }
    }
}