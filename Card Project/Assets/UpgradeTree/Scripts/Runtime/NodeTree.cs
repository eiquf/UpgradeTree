using System.Collections.Generic;
using UnityEngine;

namespace Eiquif.UpgradeTree.Runtime
{
    [CreateAssetMenu(fileName = "NodeTree", menuName = "NodeTree/NodeTree")]
    public class NodeTree : ScriptableObject
    {
        public List<Node> Nodes = new();
        public List<string> IDs = new();
    }
}