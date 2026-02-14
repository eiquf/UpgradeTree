//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditorInternal;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class IDListSetupElement : IElement<NodeTreeContext>
    {
        private readonly System.Action<ReorderableList> _assign;

        public IDListSetupElement(System.Action<ReorderableList> assign)
        {
            _assign = assign;
        }

        public void Execute(NodeTreeContext ctx)
        {
            if (ctx.IDsProp == null) return;

            var list = IDReorderableListFactory.Create(ctx);
            _assign.Invoke(list);
        }
    }
}