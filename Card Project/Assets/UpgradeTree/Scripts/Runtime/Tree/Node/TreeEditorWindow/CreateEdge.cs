namespace Eiquif.UpgradeTree.Editor.TreeWindow
{
    using Runtime.Tree;
    using UnityEditor.Experimental.GraphView;

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
}