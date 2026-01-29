using UnityEditor;
using UnityEngine;

public class NodeSection : Section
{
    private readonly NodeTreeContext _ctx;

    private NodeReorderableList _nodeList;
    private NodeValidationDrawer _validator;

    private bool _showNodesSection = true;
    private readonly EditorFlowerAnimation _anim = new();

    public NodeSection(ContextSystem ctx) : base(ctx)
    {
        _ctx = (NodeTreeContext)ctx;

        if (_ctx.NodesProp != null)
            Setup();
    }

    private void Setup()
    {
        _nodeList = new NodeReorderableList(
            _ctx.SerializedObject,
            _ctx.NodesProp,
            "Node References",
            EditorColors.SecondaryColor
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
                if (_nodeList != null)
                {
                    GUILayout.Space(4);
                    _nodeList.List.DoLayoutList();
                }

                DrawTreeValidation();
            }
        );
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