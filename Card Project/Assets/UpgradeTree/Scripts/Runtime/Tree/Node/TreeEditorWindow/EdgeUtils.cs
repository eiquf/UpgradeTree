namespace Eiquif.UpgradeTree.Editor.TreeWindow
{
    using System.Linq;
    using UnityEditor;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using Node = Runtime.Node.Node;
    using NodeTree = Runtime.Tree.NodeTree;

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
