//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public class CreateValidationHeader : IElement
    {
        public void Execute()
        {
            var rect = EditorGUILayout.GetControlRect(false, 24);
            EditorGUI.DrawRect(rect, EditorColors.WarningBgLight);
            var style = new GUIStyle(EditorStyles.boldLabel) { fontSize = 11, alignment = TextAnchor.MiddleLeft };
            style.normal.textColor = EditorColors.WarningColor;
            GUI.Label(new Rect(rect.x + 4, rect.y, rect.width, rect.height), "⚠️ Validation Issues", style);
        }
    }
}
