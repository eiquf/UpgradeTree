using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    internal sealed class NodeIDMenu : INodeIDMenu
    {
        private readonly NodeTree _tree;

        public NodeIDMenu() => _tree = GetNodeTree();

        public void Show(Node node)
        {
            var menu = new GenericMenu();

            DrawClear(menu, node);
            menu.AddSeparator(string.Empty);
            DrawIDs(menu, node);

            menu.ShowAsContext();
        }

        private static void DrawClear(GenericMenu menu, Node node)
        {
            menu.AddItem(
                new GUIContent("✕  Clear ID"),
                string.IsNullOrEmpty(node.ID.Value),
                () =>
                {
                    Undo.RecordObject(node, "Clear Node ID");
                    node.ID.Value = string.Empty;
                    EditorUtility.SetDirty(node);
                });
        }

        private void DrawIDs(GenericMenu menu, Node node)
        {
            if (_tree?.IDs == null || _tree.IDs.Count == 0)
            {
                menu.AddDisabledItem(
                    new GUIContent("No IDs available — add some above"));
                return;
            }

            foreach (var id in _tree.IDs)
            {
                var captured = id;
                var selected = node.ID.Value == id;

                menu.AddItem(
                    new GUIContent((selected ? "✓  " : "    ") + id),
                    selected,
                    () =>
                    {
                        Undo.RecordObject(node, "Set Node ID");
                        node.ID.Value = captured;
                        EditorUtility.SetDirty(node);
                    });
            }
        }
        private NodeTree GetNodeTree()
        {
            string[] guids = AssetDatabase.FindAssets("t:NodeTree");
            if (guids.Length == 0) return null;

            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<NodeTree>(path);
        }
    }
}