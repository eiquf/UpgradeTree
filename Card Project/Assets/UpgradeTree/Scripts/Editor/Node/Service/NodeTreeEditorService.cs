//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public class NodeTreeEditorService : INodeTreeEditorService
    {
        public Node CreateNode(NodeTree tree)
        {
            Undo.RecordObject(tree, "Create Node");

            var node = ScriptableObject.CreateInstance<Node>();
            node.name = $"Node_{tree.Nodes.Count}";

            AssetDatabase.AddObjectToAsset(node, tree);
            Undo.RegisterCreatedObjectUndo(node, "Create Node");

            tree.Nodes.Add(node);

            EditorUtility.SetDirty(tree);
            EditorUtility.SetDirty(node);
            AssetDatabase.SaveAssets();

            return node;
        }

        public void RemoveAllNodes(NodeTree tree)
        {
            Undo.RecordObject(tree, "Remove All Nodes");

            foreach (var node in tree.Nodes.ToArray())
            {
                if (node == null) continue;
                Undo.DestroyObjectImmediate(node);
            }

            tree.Nodes.Clear();
            EditorUtility.SetDirty(tree);
        }

        public void RemoveNode(NodeTree tree, Node node)
        {
            if (node == null) return;

            Undo.RecordObject(tree, "Remove Node");

            tree.Nodes.Remove(node);

            foreach (var n in tree.Nodes)
            {
                if (n == null) continue;
                n.NextNodes.Remove(node);
                n.PrerequisiteNodes.Remove(node);
                EditorUtility.SetDirty(n);
            }

            Undo.DestroyObjectImmediate(node);
            EditorUtility.SetDirty(tree);
        }
    }
}