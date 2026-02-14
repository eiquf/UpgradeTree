//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor.Experimental.GraphView;

namespace Eiquif.UpgradeTree.Editor
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
}