//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;


namespace Eiquif.UpgradeTree.Editor
{
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