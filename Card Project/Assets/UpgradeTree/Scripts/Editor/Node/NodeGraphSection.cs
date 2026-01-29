using UnityEditor;
using UnityEngine;

public class NodeGraphSection : Section
{
    private readonly NodeContext _ctx;

    private NodeReorderableList _nextList;
    private NodeReorderableList _prerequisiteList;
    private readonly NodeValidationDrawer _validator = new();

    private bool _showNextNodes = true;
    private bool _showPrerequisiteNodes = true;

    private readonly EditorFlowerAnimation _anim = new();

    public NodeGraphSection(ContextSystem ctx) : base(ctx)
    {
        _ctx = (NodeContext)ctx;
        Setup();
    }

    private void Setup()
    {
        if (_ctx.NextProp != null)
        {
            _nextList = new NodeReorderableList(
                _ctx.SerializedObject,
                _ctx.NextProp,
                "Next Nodes",
                EditorColors.PrimaryColor
            );
        }

        if (_ctx.PrerequisiteProp != null)
        {
            _prerequisiteList = new NodeReorderableList(
                _ctx.SerializedObject,
                _ctx.PrerequisiteProp,
                "Prerequisite Nodes",
                EditorColors.SecondaryColor
            );
        }
    }

    public override void Draw()
    {
        CollapsibleSection.Draw(
            "Next nodes",
            "⏩",
            ref _showNextNodes,
            EditorColors.PrimaryColor,
            () =>
            {
                _nextList?.List.DoLayoutList();
                _validator?.Draw(_ctx.Node.NextNodes, _ctx.Node);
            }
        );

        GUILayout.Space(8);

        CollapsibleSection.Draw(
            "Prerequisite nodes",
            "⏪",
            ref _showPrerequisiteNodes,
            EditorColors.SecondaryColor,
            () =>
            {
                _prerequisiteList?.List.DoLayoutList();
                _validator?.Draw(_ctx.Node.PrerequisiteNodes, _ctx.Node);
            }
        );

        _anim.UpdateAndDraw_flowers(_ctx.LastUpdateTime);
    }

    #region Test Feature
    private void HandleFlowerClicks()
    {
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            _anim.Spawn(Event.current.mousePosition, 10);
    }
    #endregion
}