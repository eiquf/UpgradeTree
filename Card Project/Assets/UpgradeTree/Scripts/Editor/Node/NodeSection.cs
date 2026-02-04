namespace Eiquif.UpgradeTree.Editor.Node
{
    using Eiquif.UpgradeTree.Runtime.Node;
    using UnityEditor;
    using UnityEngine;
    public class NodeSection : Section
    {
        private readonly NodeTreeContext _ctx;

        private NodeReorderableList _nodeList;
        private NodeValidationDrawer _validator;

        private bool _showNodesSection = true;

        public NodeSection(ContextSystem ctx) : base(ctx)
        {
            _ctx = (NodeTreeContext)ctx;

            Setup();
        }

        private void Setup()
        {
            _nodeList = new NodeReorderableList(
                _ctx.SerializedObject,
                _ctx.NodesProp,
                "Node References",
                EditorColors.SecondaryColor,
                _ctx
            );

            _validator = new NodeValidationDrawer();
        }

        public override void Draw()
        {
            CollapsibleSection.Draw(
                "Nodes",
                "📦",
                ref _showNodesSection,
                EditorColors.SecondaryColor,
                () =>
                {
                    GUILayout.Space(4);

                    DrawToolbar();

                    if (_nodeList != null)
                    {
                        _nodeList.List.DoLayoutList();
                    }

                    DrawTreeValidation();
                }
            );
        }
        private void DrawToolbar()
        {
            if (_ctx.Tree == null) return;

            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            GUI.backgroundColor = new Color(1f, 0.4f, 0.4f);

            if (GUILayout.Button("🗑 Delete All Nodes", GUILayout.Width(160)))
            {
                if (EditorUtility.DisplayDialog(
                    "Delete all nodes?",
                    "This will permanently delete ALL node sub-assets.\n\nThis action can be undone.",
                    "Delete",
                    "Cancel"))
                {
                    _ctx.Tree.RemoveAllNodes();
                    AssetDatabase.SaveAssets();
                }
            }

            GUI.backgroundColor = Color.white;

            GUILayout.EndHorizontal();
            GUILayout.Space(6);
        }
        private void DrawTreeValidation()
        {
            if (_ctx.Tree == null) return;

            if (_ctx.Tree.IDs == null || _ctx.Tree.IDs.Count == 0)
            {
                EditorGUILayout.HelpBox(
                    "💡 Tip: Add IDs in the section above first, then assign them to nodes",
                    MessageType.Info
                );
                return;
            }

            _validator?.Draw(_ctx.Tree.Nodes, _ctx.Tree);
        }
    }
}