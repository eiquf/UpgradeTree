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
            if (tree == null) return null;

            Undo.RecordObject(tree, "Create Node");

            var node = ScriptableObject.CreateInstance<Node>();
            node.name = "Node";

            Undo.RegisterCreatedObjectUndo(node, "Create Node");

            tree.Nodes.Add(node);
            AssetDatabase.AddObjectToAsset(node, tree);

            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();

            RefreshAllEditors(tree);

            return node;
        }

        public void RemoveNode(NodeTree tree, Node node)
        {
            if (tree == null || node == null)
                return;

            Undo.IncrementCurrentGroup();
            int group = Undo.GetCurrentGroup();

            Undo.RecordObject(tree, "Remove Node");

            foreach (var n in tree.Nodes)
            {
                if (n == null || n == node) continue;

                Undo.RecordObject(n, "Remove Node Links");
                n.NextNodes.Remove(node);
                n.PrerequisiteNodes.Remove(node);
                EditorUtility.SetDirty(n);
            }

            tree.Nodes.Remove(node);
            EditorUtility.SetDirty(tree);

            Undo.DestroyObjectImmediate(node);
            Undo.CollapseUndoOperations(group);

            AssetDatabase.SaveAssets();

            RefreshAllEditors(tree);
        }

        public void RemoveAllNodes(NodeTree tree)
        {
            if (tree == null) return;

            Undo.IncrementCurrentGroup();
            int group = Undo.GetCurrentGroup();

            Undo.RecordObject(tree, "Remove All Nodes");

            foreach (var node in tree.Nodes)
            {
                if (node == null) continue;
                Undo.DestroyObjectImmediate(node);
            }

            tree.Nodes.Clear();
            EditorUtility.SetDirty(tree);

            Undo.CollapseUndoOperations(group);

            AssetDatabase.SaveAssets();

            RefreshAllEditors(tree);
        }

        private static void RefreshAllEditors(NodeTree tree)
        {
            var editors =
                Resources.FindObjectsOfTypeAll<UpgradeTreeEditor>();

            foreach (var editor in editors)
            {
                if (editor == null) continue;
                editor.ForceReloadFromAsset(tree);
                editor.Repaint();
            }
        }
    }
}