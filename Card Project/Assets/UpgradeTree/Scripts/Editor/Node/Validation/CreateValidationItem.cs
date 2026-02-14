//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public struct ValidationItemData
    {
        public string Text;
        public Color Color;
    }
    public class CreateValidationItem : IElement<ValidationItemData>
    {
        public void Execute(ValidationItemData data)
        {
            EditorGUILayout.BeginHorizontal();

            var dotRect = EditorGUILayout.GetControlRect(
                GUILayout.Width(8),
                GUILayout.Height(16)
            );

            EditorGUI.DrawRect(
                new Rect(dotRect.x, dotRect.y + 4, 8, 8),
                data.Color
            );

            EditorGUILayout.LabelField(data.Text);

            EditorGUILayout.EndHorizontal();
        }
    }
}