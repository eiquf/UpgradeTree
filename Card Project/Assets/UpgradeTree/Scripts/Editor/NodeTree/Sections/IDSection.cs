using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class IDSection : Section
{
    private readonly NodeTreeContext _ctx;
    private ReorderableList _idList;
    private bool _showIDSection = true;

    private readonly EditorFlowerAnimation _anim = new();

    public IDSection(ContextSystem ctx) : base(ctx) 
    {
        _ctx = (NodeTreeContext)ctx;
        if (_ctx.IDsProp != null) SetupIDList(); 
    }
    public override void Draw()
    {
        CollapsibleSection.Draw("ID Database", "🆔", ref _showIDSection, EditorColors.PrimaryColor, () =>
        {
            if (_idList != null)
            {
                GUILayout.Space(4);
                _idList.DoLayoutList();
            }
            DrawIDStats();
        });

    }
    private void DrawIDStats()
    {
        if (_ctx.IDsProp == null || _ctx.IDsProp.arraySize == 0) return;

        GUILayout.Space(8);

        var usedCount = _ctx.Tree.IDs?.Count(id =>
            _ctx.Tree.Nodes?.Any(n => n != null && n.ID.Value == id) ?? false) ?? 0;
        var unusedCount = (_ctx.Tree.IDs?.Count ?? 0) - usedCount;

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        EditorDrawUtils.DrawMiniStatBadge($"✓ {usedCount} used", EditorColors.SuccessColor);
        GUILayout.Space(8);

        if (unusedCount > 0)
        {
            EditorDrawUtils.DrawMiniStatBadge($"○ {unusedCount} unused", EditorColors.WarningColor);
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }
    public void SetupIDList()
    {
        if (_ctx.IDsProp == null) return;

        _idList = new ReorderableList(_ctx.SerializedObject, _ctx.IDsProp, true, true, true, true)
        {
            drawHeaderCallback = rect =>
            {
                var labelRect = new Rect(rect.x, rect.y, rect.width - 60, rect.height);
                var countRect = new Rect(rect.xMax - 55, rect.y, 55, rect.height);

                EditorGUI.LabelField(labelRect, "Registered IDs", EditorStyles.boldLabel);

                EditorDrawUtils.DrawCountBadge(countRect, _ctx.IDsProp.arraySize, EditorColors.PrimaryColor);
            },

            drawElementCallback = (rect, index, active, focused) =>
            {
                if (_ctx.IDsProp == null) return;
                var element = _ctx.IDsProp.GetArrayElementAtIndex(index);
                if (element == null) return;

                rect.y += 2;
                rect.height = EditorGUIUtility.singleLineHeight;

                var id = element.stringValue;
                var usageCount = _ctx.Tree?.Nodes?.Count(n => n != null && n.ID.Value == id) ?? 0;

                var bgRect = new Rect(rect.x - 4, rect.y - 2, rect.width + 8, rect.height + 4);
                var bgColor = usageCount == 0 ? EditorColors.WarningBgLight : EditorColors.SuccessBgLight;
                EditorGUI.DrawRect(bgRect, bgColor);

                var indicatorRect = new Rect(rect.x - 4, rect.y - 2, 3, rect.height + 4);
                EditorGUI.DrawRect(indicatorRect, usageCount == 0 ? EditorColors.WarningColor : EditorColors.SuccessColor);

                var idRect = new Rect(rect.x + 4, rect.y, rect.width - 70, rect.height);
                element.stringValue = EditorGUI.TextField(idRect, element.stringValue);

                var countRect = new Rect(rect.xMax - 60, rect.y, 55, rect.height);
                var countStyle = new GUIStyle(EditorStyles.miniLabel)
                {
                    alignment = TextAnchor.MiddleRight,
                    fontStyle = usageCount == 0 ? FontStyle.Normal : FontStyle.Bold
                };
                countStyle.normal.textColor = usageCount == 0 ? EditorColors.WarningColor : EditorColors.SuccessColor;
                EditorGUI.LabelField(countRect, $"×{usageCount} used", countStyle);
            },

            onAddCallback = list =>
            {
                var index = list.serializedProperty.arraySize;
                list.serializedProperty.InsertArrayElementAtIndex(index);
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                element.stringValue = $"NewID_{index}";

                _anim.Spawn(Event.current.mousePosition, 5);
            },

            onRemoveCallback = list =>
            {
                var id = _ctx.IDsProp.GetArrayElementAtIndex(list.index).stringValue;
                var usedBy = _ctx.Tree?.Nodes?.Where(n => n != null && n.ID.Value == id).ToList();

                if (usedBy != null && usedBy.Count > 0)
                {
                    if (!EditorUtility.DisplayDialog(
                        "⚠️ ID In Use",
                        $"ID '{id}' is used by {usedBy.Count} nodes.\n\nRemoving it will leave those nodes without an ID.",
                        "Remove Anyway", "Cancel"))
                    {
                        return;
                    }
                }

                ReorderableList.defaultBehaviours.DoRemoveButton(list);
            },

            elementHeight = 24,

            drawElementBackgroundCallback = (rect, index, active, focused) =>
            {
                if (active)
                    EditorGUI.DrawRect(rect, new Color(0.3f, 0.5f, 0.8f, 0.3f));
                else if (index % 2 == 0)
                    EditorGUI.DrawRect(rect, new Color(0f, 0f, 0f, 0.1f));
            }
        };
    }
}