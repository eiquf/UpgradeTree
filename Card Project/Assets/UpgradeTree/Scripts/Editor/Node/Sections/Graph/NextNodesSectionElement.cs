//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class NextNodesSectionElement : IElement<NodeContext>
    {
        private bool _expanded = true;
        private NodeReorderableList _list;
        private readonly NodeValidationDrawer _validator = new();

        public void Execute(NodeContext ctx)
        {
            if (ctx?.NextProp == null) return;

            _list ??= new NodeReorderableList(
                ctx.SerializedObject,
                ctx.NextProp,
                "Next Nodes",
                EditorColors.PrimaryColor,
                ctx,
                false
            );

            CollapsibleSection.Draw(
                "Next nodes",
                "⏩",
                ref _expanded,
                EditorColors.PrimaryColor,
                () =>
                {
                    _list.List.DoLayoutList();
                    _validator.Draw(ctx.Node.NextNodes, ctx.Node);
                }
            );

            GUILayout.Space(8);
        }
    }
}