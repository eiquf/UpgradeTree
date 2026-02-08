using Eiquif.UpgradeTree.Runtime;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class NodeHeaderContext
    {
        public Rect Rect;
        public string Name = string.Empty;
        public NodeContext NodeContext = null!;
    }
}