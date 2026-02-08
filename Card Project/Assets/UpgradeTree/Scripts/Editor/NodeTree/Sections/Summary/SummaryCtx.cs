using Eiquif.UpgradeTree.Runtime;
using System.Collections.Generic;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class SummaryCtx
    {
        public NodeTreeContext TreeContext = null!;

        public int TotalNodes;
        public int TotalIds;
        public int AssignedNodes;

        public List<SummaryGroup> Groups = new();
        public EditorFlowerAnimation Anim = null!;
    }

}