using System.Linq;
using UnityEditor;
using UnityEngine;

public class ActionsSection : Section
{
    private readonly NodeTreeContext _ctx;
    private readonly EditorFlowerAnimation _anim = new();

    private bool _showActionsSection = true;

    private readonly FoldoutState _foldouts = new();
    private readonly ActionExecutor _executor;
    private readonly ActionGroupDrawer _groupDrawer;

    public ActionsSection(ContextSystem ctx) : base(ctx)
    {
        _ctx = (NodeTreeContext)ctx;

        _executor = new ActionExecutor();
        _groupDrawer = new ActionGroupDrawer(
            _foldouts,
            new ActionNodeGroupDrawer(_foldouts, _executor)
        );
    }

    public override void Draw()
    {
        CollapsibleSection.Draw("Actions", "⚡", ref _showActionsSection, EditorColors.AccentColor, DrawContent);
    }

    private void DrawContent()
    {
        if (_ctx.Tree.Nodes == null || _ctx.Tree.Nodes.Count == 0)
        {
            EditorDrawUtils.DrawEmptyState("📭", "No Nodes Yet", "Add nodes above to enable actions");
            return;
        }

        if (_ctx.Tree.Nodes.All(n => n == null || string.IsNullOrEmpty(n.ID.Value)))
        {
            EditorDrawUtils.DrawEmptyState("⚠️", "No Configured Nodes", "Assign IDs first");
            DrawDisabledAddButton();
            return;
        }

        DrawActionGroups();
        DrawAddButton();
    }

    private void DrawActionGroups()
    {
        var groups = _ctx.Tree.Actions
            .GroupBy(a => a.Method.Name);

        foreach (var group in groups)
        {
            _groupDrawer.Draw(
                group.Key,
                group.ToList(),
                _ctx.Tree.Nodes,
                () =>
                {
                    Undo.RecordObject(_ctx.Tree, "Remove Actions");
                    _ctx.Tree.Actions.RemoveAll(a => a.Method.Name == group.Key);
                    EditorUtility.SetDirty(_ctx.Tree);
                }
            );

            GUILayout.Space(4);
        }
    }

    private void DrawAddButton()
    {
        if (GUILayout.Button("➕ Add Action", GUILayout.Height(28)))
        {
            //_ctx.Tree.Actions.AddAction(SampleAction);
            _anim.Spawn(GUILayoutUtility.GetLastRect().center, 8);
        }
    }

    private void DrawDisabledAddButton()
    {
        EditorGUI.BeginDisabledGroup(true);
        GUILayout.Button("➕ Add Action (assign IDs first)", GUILayout.Height(28));
        EditorGUI.EndDisabledGroup();
    }

    private void SampleAction(Node node)
    {
        Debug.Log($"[Action] Invoked on: {node.name} (ID: {node.ID.Value})");
    }
}

