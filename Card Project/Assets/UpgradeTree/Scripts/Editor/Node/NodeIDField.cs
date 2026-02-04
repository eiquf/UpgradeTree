namespace Eiquif.UpgradeTree.Editor.Node
{
    using Eiquif.UpgradeTree.Runtime.Node;
    using UnityEditor;
    using UnityEngine;
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