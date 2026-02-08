//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public static class NodeIDField
    {
        public static string Draw(
            Rect rect,
            string value,
            Node node,
            ContextSystem ctx)
        {
            var fieldRect = new Rect(
                rect.x,
                rect.y,
                rect.width - 110,
                rect.height
            );

            var buttonRect = new Rect(
                rect.xMax - 105,
                rect.y,
                105,
                rect.height
            );

            value = EditorGUI.TextField(fieldRect, value);

            var label = string.IsNullOrEmpty(value) ? "Select ID..." : value;

            if (EditorGUI.DropdownButton(
                buttonRect,
                new GUIContent(label),
                FocusType.Keyboard))
            {
                ctx.IDMenu.Show(node);
            }

            return value;
        }
    }
}