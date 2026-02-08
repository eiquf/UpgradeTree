//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class ValidationCtx
    {
        public List<Node> Nodes = null!;
        public Object UndoTarget = null!;
        public int NullCount;
        public int NoIdCount;
        public int DuplicateCount;

        public System.Action<Object, int> ApplyChanges;
    }
}