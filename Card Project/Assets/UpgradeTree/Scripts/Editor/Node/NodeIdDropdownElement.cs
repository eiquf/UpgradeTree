//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public class NodeIdDropdownElement : IElement<NodeListElementContext>
    {
        public void Execute(NodeListElementContext ctx)
        {
            if (ctx?.Node == null) return;

            var idRect = new Rect(ctx.Rect.xMax - 120, ctx.Rect.y, 115, ctx.Rect.height);
            var label = ctx.HasId ? ctx.Node.ID.Value : "Select ID...";

            var style = new GUIStyle(EditorStyles.popup)
            {
                fontSize = 10
            };

            if (EditorGUI.DropdownButton(idRect, new GUIContent(label),
                FocusType.Keyboard, style))
            {
                ctx.Ctx.IDMenu.Show(ctx.Node);
            }
        }
    }
}