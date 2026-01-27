using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class NodeSection : Section
{
    private ReorderableList _nodeList;
    private bool _showNodesSection = true;

    private readonly EditorFlowerAnimation _anim = new();

    public NodeSection(NodeTreeContext ctx) : base(ctx)
    {
        if (ctx.NodesProp != null) SetupNodeList();
    }
    public override void Draw()
    {
        CollapsibleSection.Draw("Nodes", "📦", ref _showNodesSection, EditorColors.SecondaryColor, () =>
        {
            if (_nodeList != null)
            {
                GUILayout.Space(4);
                _nodeList.DoLayoutList();
            }
            DrawNodeValidation();
        });
    }
    private void SetupNodeList()
    {
        if (ctx.NodesProp == null) return;

        _nodeList = new ReorderableList(ctx.SerializedObject, ctx.NodesProp, true, true, true, true)
        {
            drawHeaderCallback = rect =>
            {
                var labelRect = new Rect(rect.x, rect.y, rect.width - 60, rect.height);
                var countRect = new Rect(rect.xMax - 55, rect.y, 55, rect.height);

                EditorGUI.LabelField(labelRect, "Node References", EditorStyles.boldLabel);
                EditorDrawUtils.DrawCountBadge(countRect, ctx.NodesProp.arraySize, EditorColors.SecondaryColor);
            },

            drawElementCallback = (rect, index, active, focused) =>
            {
                var element = ctx.NodesProp.GetArrayElementAtIndex(index);
                rect.y += 2;
                rect.height = EditorGUIUtility.singleLineHeight;

                var node = element.objectReferenceValue as Node;
                var hasId = node != null && !string.IsNullOrEmpty(node.ID.Value);

                var indicatorRect = new Rect(rect.x - 4, rect.y - 2, 3, rect.height + 4);
                var indicatorColor = node == null ? EditorColors.ErrorColor : (hasId ? EditorColors.SuccessColor : EditorColors.WarningColor);
                EditorGUI.DrawRect(indicatorRect, indicatorColor);

                var bgRect = new Rect(rect.x - 1, rect.y - 2, rect.width + 2, rect.height + 4);
                var bgColor = node == null ? EditorColors.ErrorBgLight : (hasId ? EditorColors.SuccessBgLight : EditorColors.WarningBgLight);
                EditorGUI.DrawRect(bgRect, bgColor);

                var nodeRect = new Rect(rect.x + 4, rect.y, rect.width - 130, rect.height);
                EditorGUI.PropertyField(nodeRect, element, GUIContent.none);

                if (node != null)
                {
                    var idRect = new Rect(rect.xMax - 120, rect.y, 115, rect.height);
                    var buttonLabel = hasId ? node.ID.Value : "Select ID...";
                    var buttonStyle = new GUIStyle(EditorStyles.popup)
                    {
                        fontSize = 10
                    };

                    if (EditorGUI.DropdownButton(idRect, new GUIContent(buttonLabel), FocusType.Keyboard, buttonStyle))
                    {
                        ShowIDSelectionMenu(node);
                    }
                }
            },

            onAddCallback = list =>
            {
                var index = list.serializedProperty.arraySize;
                list.serializedProperty.InsertArrayElementAtIndex(index);

                _anim.Spawn(Event.current.mousePosition, 5);
            },

            elementHeight = 24,

            drawElementBackgroundCallback = (rect, index, active, focused) =>
            {
                if (active)
                {
                    EditorGUI.DrawRect(rect, new Color(0.4f, 0.3f, 0.6f, 0.3f));
                }
                else if (index % 2 == 0)
                {
                    EditorGUI.DrawRect(rect, new Color(0f, 0f, 0f, 0.1f));
                }
            }
        };
    }
    private void ShowIDSelectionMenu(Node node)
    {
        var menu = new GenericMenu();

        menu.AddItem(
            new GUIContent("✕  Clear ID"),
            string.IsNullOrEmpty(node.ID.Value),
            () =>
            {
                Undo.RecordObject(node, "Clear Node ID");
                node.ID.Value = string.Empty;
                EditorUtility.SetDirty(node);
            });

        menu.AddSeparator(string.Empty);

        if (ctx.Tree.IDs != null && ctx.Tree.IDs.Count > 0)
        {
            foreach (var id in ctx.Tree.IDs)
            {
                var capturedId = id;
                var isSelected = node.ID.Value == id;
                var prefix = isSelected ? "✓  " : "    ";

                menu.AddItem(
                    new GUIContent(prefix + id),
                    isSelected,
                    () =>
                    {
                        Undo.RecordObject(node, "Set Node ID");
                        node.ID.Value = capturedId;
                        EditorUtility.SetDirty(node);
                    });
            }
        }
        else
        {
            menu.AddDisabledItem(new GUIContent("No IDs available — add some above"));
        }

        menu.ShowAsContext();
    }
    private void DrawNodeValidation()
    {
        if (ctx.Tree.IDs is null || ctx.Tree.IDs.Count is 0)
        {
            EditorGUILayout.HelpBox("💡 Tip: Add IDs in the section above first, then assign them to nodes", MessageType.Info);
            return;
        }

        if (ctx.Tree.Nodes is null || ctx.Tree.Nodes.Count is 0) return;

        var nodesWithoutId = ctx.Tree.Nodes.Where(n => n != null && string.IsNullOrEmpty(n.ID.Value)).ToList();
        var nullNodes = ctx.Tree.Nodes.Count(n => n == null);

        if (nodesWithoutId.Count is 0 && nullNodes is 0) return;

        GUILayout.Space(8);

        EditorGUILayout.BeginVertical(EditorStyleCache.CardStyle);

        var warningRect = EditorGUILayout.GetControlRect(false, 24);
        EditorGUI.DrawRect(warningRect, EditorColors.WarningBgLight);
        var warningStyle = new GUIStyle(EditorStyles.boldLabel) { fontSize = 11 };
        warningStyle.normal.textColor = EditorColors.WarningColor;
        GUI.Label(new Rect(warningRect.x + 8, warningRect.y, warningRect.width, warningRect.height), "⚠️ Validation Issues", warningStyle);

        GUILayout.Space(4);

        if (nullNodes > 0)
            DrawValidationItem($"{nullNodes} empty slot(s)", EditorColors.ErrorColor, "These will be ignored at runtime");

        if (nodesWithoutId.Count > 0)
            DrawValidationItem($"{nodesWithoutId.Count} node(s) without ID", EditorColors.WarningColor, "Assign IDs for proper functionality");

        GUILayout.Space(8);

        if (GUILayout.Button("🧹 Clean Empty Slots", GUILayout.Height(24)))
        {
            Undo.RecordObject(ctx.Tree, "Clean Null Nodes");
            var removed = ctx.Tree.Nodes.Count(n => n == null);
            ctx.Tree.Nodes = ctx.Tree.Nodes.Where(n => n != null).ToList();
            EditorUtility.SetDirty(ctx.Tree);

            if (removed > 0)
            {
                var rect = GUILayoutUtility.GetLastRect();
                _anim.Spawn(new Vector2(rect.center.x, rect.center.y), removed * 3);
            }
        }

        EditorGUILayout.EndVertical();
    }
    private void DrawValidationItem(string message, Color color, string tooltip)
    {
        EditorGUILayout.BeginHorizontal();

        var dotRect = EditorGUILayout.GetControlRect(GUILayout.Width(8), GUILayout.Height(16));
        EditorGUI.DrawRect(new Rect(dotRect.x, dotRect.y + 4, 8, 8), color);

        var content = new GUIContent(message, tooltip);
        EditorGUILayout.LabelField(content);

        EditorGUILayout.EndHorizontal();
    }
}