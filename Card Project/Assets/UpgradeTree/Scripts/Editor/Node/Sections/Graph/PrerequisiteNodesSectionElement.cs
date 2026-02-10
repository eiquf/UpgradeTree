//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class PrerequisiteNodesSectionElement : IElement<NodeContext>
    {
        private bool _expanded = true;
        private NodeReorderableList _list;
        private readonly NodeValidationDrawer _validator = new();

        public void Execute(NodeContext ctx)
        {
            if (ctx?.PrerequisiteProp == null) return;

            _list ??= new NodeReorderableList(
                ctx.SerializedObject,
                ctx.PrerequisiteProp,
                "Prerequisite Nodes",
                EditorColors.SecondaryColor,
                ctx,
                false
            );

            CollapsibleSection.Draw(
                "Prerequisite nodes",
                "⏪",
                ref _expanded,
                EditorColors.SecondaryColor,
                () =>
                {
                    _list.List.DoLayoutList();
                    _validator.Draw(ctx.Node.PrerequisiteNodes, ctx.Node);
                }
            );
        }
    }
}