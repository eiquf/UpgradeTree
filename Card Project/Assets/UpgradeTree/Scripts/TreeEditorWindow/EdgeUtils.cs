//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Node = Eiquif.UpgradeTree.Runtime.Node;

namespace Eiquif.UpgradeTree.Editor
{
    static class EdgeUtils
    {
        public static bool TryGetNodes(
            Edge edge,
            out Node from,
            out Node to)
        {
            from = null;
            to = null;

            if (edge?.output?.node is not UpgradeNodeView fromView)
                return false;

            if (edge?.input?.node is not UpgradeNodeView toView)
                return false;

            from = fromView.Data;
            to = toView.Data;
            return true;
        }

        public static void RecordUndo(
            NodeTree tree,
            Node from,
            Node to,
            string action)
        {
            var objects = new Object[]
            {
                tree,
                from,
                to
            }.Where(o => o != null).ToArray();

            if (objects.Length == 0)
                return;

            Undo.RecordObjects(objects, action);
        }

        public static void MarkDirty(
            NodeTree tree,
            Node from,
            Node to)
        {
            if (tree != null) EditorUtility.SetDirty(tree);
            if (from != null) EditorUtility.SetDirty(from);
            if (to != null) EditorUtility.SetDirty(to);
        }
    }
}
