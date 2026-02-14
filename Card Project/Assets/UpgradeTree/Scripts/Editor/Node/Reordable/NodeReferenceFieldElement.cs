//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public class NodeReferenceFieldElement : IElement<NodeListElementContext>
    {
        public void Execute(NodeListElementContext ctx)
        {
            if (ctx == null) return;

            EditorGUI.PropertyField(
                new Rect(ctx.Rect.x + 4, ctx.Rect.y, ctx.Rect.width - 130, ctx.Rect.height),
                ctx.Property,
                GUIContent.none
            );
        }
    }
}