namespace Eiquif.UpgradeTree.Editor.TreeWindow
{
    using Runtime.Node;
    using Runtime.Tree;
    using UnityEditor;
    using UnityEngine;

    public class CreateNode : IElement
    {
        private readonly NodeTree _tree;
        public CreateNode(NodeTree tree) => _tree = tree;

        public void Execute()
        {
            if (_tree == null) return;

            Undo.RecordObject(_tree, "Create Node");

            var node = ScriptableObject.CreateInstance<Node>();
            node.name = "Node";

            _tree.Nodes.Add(node);
            AssetDatabase.AddObjectToAsset(node, _tree);

            EditorUtility.SetDirty(_tree);
            AssetDatabase.SaveAssets();
        }
    }
}
