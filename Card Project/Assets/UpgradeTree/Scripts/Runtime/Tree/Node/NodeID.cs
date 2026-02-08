//***************************************************************************************
// Writer: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System;

namespace Eiquif.UpgradeTree.Runtime
{
    [Serializable]
    public class NodeID
    {
        public string Value;
        public NodeID() { }
        public NodeID(string value) => Value = value;
    }
}