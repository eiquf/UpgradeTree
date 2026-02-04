namespace Eiquif.UpgradeTree.Runtime.Node
{
    using System;

    [Serializable]
    public class NodeID
    {
        public string Value;
        public NodeID() { }
        public NodeID(string value) => Value = value;
    }
}