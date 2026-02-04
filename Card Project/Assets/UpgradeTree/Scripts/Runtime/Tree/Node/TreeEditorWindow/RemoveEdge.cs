namespace Eiquif.UpgradeTree.Editor.TreeWindow
{
    using UnityEditor.Experimental.GraphView;
    using Tree = Runtime.Tree.NodeTree;
    public class RemoveEdge : IElement<Edge>
    {
        private readonly Tree _tree;
        public RemoveEdge(Tree tree) => _tree = tree;

        public void Execute(Edge edge)
        {
            if (!EdgeUtils.TryGetNodes(edge, out var from, out var to))
                return;

            if (!from.NextNodes.Contains(to))
                return;

            EdgeUtils.RecordUndo(_tree, from, to, "Remove Edge");

            from.NextNodes.Remove(to);
            to.PrerequisiteNodes.Remove(from);

            EdgeUtils.MarkDirty(_tree, from, to);
        }
    }
}