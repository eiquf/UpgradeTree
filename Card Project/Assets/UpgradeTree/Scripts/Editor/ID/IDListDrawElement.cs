//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditorInternal;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class IDListDrawElement : IElement<NodeTreeContext>
    {
        private readonly System.Func<ReorderableList> _getList;

        public IDListDrawElement(System.Func<ReorderableList> getList)
        {
            _getList = getList;
        }

        public void Execute(NodeTreeContext ctx)
        {
            var list = _getList();
            if (list == null) return;

            GUILayout.Space(4);
            list.DoLayoutList();
        }
    }
}