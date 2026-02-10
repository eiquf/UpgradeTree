//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;

namespace Eiquif.UpgradeTree.Editor
{
    public class RemoveNode : IElement<UpgradeNodeView>
    {
        private readonly NodeTree _tree;
        public RemoveNode(NodeTree tree) => _tree = tree;
        public void Execute(UpgradeNodeView view)
        {
            if (_tree == null || view == null) return;

            var node = view.Data;
            if (node == null) return;

            Undo.RecordObject(_tree, "Remove Node");

            foreach (var n in _tree.Nodes)
            {
                if (n == null || n == node) continue;

                Undo.RecordObject(n, "Remove Node Links");
                n.NextNodes.Remove(node);
                n.PrerequisiteNodes.Remove(node);
                EditorUtility.SetDirty(n);
            }

            _tree.Nodes.Remove(node);
            EditorUtility.SetDirty(_tree);

            Undo.DestroyObjectImmediate(node);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}