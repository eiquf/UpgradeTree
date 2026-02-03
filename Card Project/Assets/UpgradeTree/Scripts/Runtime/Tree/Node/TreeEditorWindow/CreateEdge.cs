using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor.TreeWindow
{
    public class CreateEdge : IElement<Edge>
    {
        private readonly NodeTree _tree;
        public CreateEdge(NodeTree tree) => _tree = tree;

        public void Execute(Edge edge)
        {
            if (!EdgeUtils.TryGetNodes(edge, out var from, out var to))
                return;

            if (from.NextNodes.Contains(to))
                return;

            EdgeUtils.RecordUndo(_tree, from, to, "Add Edge");

            from.NextNodes.Add(to);
            to.PrerequisiteNodes.Add(from);

            EdgeUtils.MarkDirty(_tree, from, to);
        }
    }

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
            Undo.RecordObjects(
                new Object[] { tree, from, to },
                action
            );
        }

        public static void MarkDirty(
            NodeTree tree,
            Node from,
            Node to)
        {
            EditorUtility.SetDirty(tree);
            EditorUtility.SetDirty(from);
            EditorUtility.SetDirty(to);
        }
    }

}
