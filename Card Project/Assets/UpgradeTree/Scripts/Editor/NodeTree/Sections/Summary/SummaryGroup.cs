//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using System.Collections.Generic;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class SummaryGroup
    {
        public string IdKey = string.Empty;
        public List<Node> Nodes = new();
        public bool IsNoId;
        public bool HasDuplicates;
    }

}