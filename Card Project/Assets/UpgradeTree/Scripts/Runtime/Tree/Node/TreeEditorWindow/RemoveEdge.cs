using UnityEditor.Experimental.GraphView;

namespace Eiquif.UpgradeTree.Editor.TreeWindow
{
    public class RemoveEdge : IElement<Edge>
    {
        private readonly NodeTree _tree;
        public RemoveEdge(NodeTree tree) => _tree = tree;

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
