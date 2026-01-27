using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ActionsSection : Section
{
    private readonly EditorFlowerAnimation _anim = new();

    private readonly Dictionary<string, bool> _actionFoldouts = new();
    private readonly Dictionary<string, bool> _idNodeFoldouts = new();

    private bool _showActionsSection = true;

    public ActionsSection(NodeTreeContext ctx) : base(ctx) { }

    public override void Draw()
    {
        CollapsibleSection.Draw("Actions", "⚡", ref _showActionsSection, EditorColors.AccentColor, () =>
        {
            DrawActions();
        });
    }
    private void DrawActions()
    {
        if (ctx.Tree.Nodes == null || ctx.Tree.Nodes.Count == 0)
        {
            EditorDrawUtils.DrawEmptyState("📭", "No Nodes Yet", "Add nodes above to enable actions");
            return;
        }

        var nodesWithId = ctx.Tree.Nodes.Count(n => n != null && !string.IsNullOrEmpty(n.ID.Value));

        if (nodesWithId is 0)
        {
            EditorDrawUtils.DrawEmptyState("⚠️", "No Configured Nodes", "Assign IDs to nodes before adding actions");

            EditorGUI.BeginDisabledGroup(true);
            GUILayout.Button("➕ Add Action (assign IDs first)", GUILayout.Height(28));
            EditorGUI.EndDisabledGroup();
            return;
        }

        if (ctx.Tree.Actions == null || ctx.Tree.Actions.Count == 0)
            EditorDrawUtils.DrawEmptyState("⚡", "No Actions", "Click below to add your first action");
        else
        {
            GUILayout.Space(4);

            var actionGroups = ctx.Tree.Actions.GroupBy(a => a.Method.Name).ToList();
            foreach (var actionGroup in actionGroups)
            {
                DrawActionGroup(actionGroup.Key, actionGroup.ToList());
                GUILayout.Space(4);
            }
        }

        GUILayout.Space(8);

        DrawButton();
    }
    private void DrawButton()
    {
        var buttonStyle = new GUIStyle(GUI.skin.button)
        {
            fontSize = 12,
            fontStyle = FontStyle.Bold
        };

        if (GUILayout.Button("➕ Add Action", buttonStyle, GUILayout.Height(28)))
        {
            ctx.Tree.AddAction(SampleAction);

            var rect = GUILayoutUtility.GetLastRect();
            _anim.Spawn(new Vector2(rect.center.x, rect.center.y), 8);
        }
    }
    private void DrawActionGroup(string actionName, List<Action<Node>> actions)
    {
        if (!_actionFoldouts.ContainsKey(actionName))
            _actionFoldouts[actionName] = false;

        EditorGUILayout.BeginVertical(EditorStyleCache.CardStyle);

        EditorGUILayout.BeginHorizontal();

        var foldoutRect = EditorGUILayout.GetControlRect(GUILayout.Width(20));
        var arrow = _actionFoldouts[actionName] ? "▼" : "▶";
        if (GUI.Button(foldoutRect, arrow, EditorStyles.label))
            _actionFoldouts[actionName] = !_actionFoldouts[actionName];

        var labelStyle = new GUIStyle(EditorStyles.boldLabel) { fontSize = 11 };
        EditorGUILayout.LabelField($"🎯 {actionName}", labelStyle);

        EditorDrawUtils.DrawCountBadge(EditorGUILayout.GetControlRect(GUILayout.Width(30), GUILayout.Height(18)),
            actions.Count, EditorColors.AccentColor);

        GUILayout.FlexibleSpace();

        var deleteStyle = new GUIStyle(EditorStyles.miniButton);
        deleteStyle.normal.textColor = EditorColors.ErrorColor;
        if (GUILayout.Button("✕", deleteStyle, GUILayout.Width(24)))
        {
            Undo.RecordObject(ctx.Tree, "Remove Actions");
            ctx.Tree.Actions.RemoveAll(a => a.Method.Name == actionName);
            EditorUtility.SetDirty(ctx.Tree);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            return;
        }

        EditorGUILayout.EndHorizontal();

        if (_actionFoldouts[actionName])
        {
            GUILayout.Space(4);
            EditorGUI.indentLevel++;
            DrawActionNodes(actionName, actions.First());
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndVertical();
    }
    private void DrawActionNodes(string actionName, Action<Node> action)
    {
        var nodesByID = ctx.Tree.Nodes
            .Where(n => n != null)
            .GroupBy(n => string.IsNullOrEmpty(n.ID.Value) ? "⚠️ [No ID]" : n.ID.Value)
            .OrderBy(g => g.Key == "⚠️ [No ID]" ? 1 : 0)
            .ThenBy(g => g.Key);

        foreach (var group in nodesByID)
        {
            DrawNodeGroup(actionName, group.Key, group.ToList(), action);
        }
    }
    private void DrawNodeGroup(string actionName, string groupKey, List<Node> nodes, Action<Node> action)
    {
        var idKey = $"{actionName}_{groupKey}";
        var isNoId = groupKey == "⚠️ [No ID]";

        if (!_idNodeFoldouts.ContainsKey(idKey))
            _idNodeFoldouts[idKey] = false;

        EditorGUILayout.BeginHorizontal();

        var colorRect = EditorGUILayout.GetControlRect(GUILayout.Width(4), GUILayout.Height(18));
        EditorGUI.DrawRect(colorRect, isNoId ? EditorColors.WarningColor : EditorColors.SuccessColor);

        GUILayout.Space(4);

        _idNodeFoldouts[idKey] = EditorGUILayout.Foldout(_idNodeFoldouts[idKey], $"{groupKey}", true);

        var countStyle = new GUIStyle(EditorStyles.miniLabel);
        countStyle.normal.textColor = Color.grey;
        GUILayout.Label($"({nodes.Count})", countStyle, GUILayout.Width(30));

        GUILayout.FlexibleSpace();

        if (!isNoId)
        {
            var runStyle = new GUIStyle(EditorStyles.miniButton) { fontSize = 10 };
            if (GUILayout.Button("▶ Run All", runStyle, GUILayout.Width(60)))
            {
                foreach (var node in nodes)
                    action.Invoke(node);
            }
        }

        EditorGUILayout.EndHorizontal();

        if (_idNodeFoldouts[idKey])
        {
            EditorGUI.indentLevel++;
            foreach (var node in nodes)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.ObjectField(node, typeof(Node), false);

                if (GUILayout.Button("▶", GUILayout.Width(24)))
                    action.Invoke(node);

                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;
        }
    }
    private void SampleAction(Node node) => Debug.Log($"[Action] Invoked on: {node.name} (ID: {node.ID.Value})");
}