using System.Linq;
using UnityEditor;
using UnityEngine;

public class NodeTreeEditorNames : EditorNames
{
    private readonly string _name;
    private readonly EditorFlowerAnimation _anim = new();

    public NodeTreeEditorNames(NodeTreeContext context, string name, double lastUpdateTime)
        : base(context, lastUpdateTime)
    {
        _name = name;
    }

    protected override void DrawBackground(Rect rect)
    {
        EditorDrawUtils.DrawGradientRect(rect, new Color(0.2f, 0.25f, 0.35f), new Color(0.15f, 0.18f, 0.25f));
    }

    protected override void DrawBorders(Rect rect)
    {
        EditorDrawUtils.DrawBorder(rect, EditorColors.PrimaryColor, 2);
    }

    protected override void DrawIcons(Rect rect)
    {
        var flowerStyle = new GUIStyle { fontSize = 16, alignment = TextAnchor.MiddleCenter };
        GUI.Label(new Rect(rect.x + 4, rect.y + 2, 20, 20), "🌸", flowerStyle);
        GUI.Label(new Rect(rect.xMax - 24, rect.y + 2, 20, 20), "🌺", flowerStyle);
        GUI.Label(new Rect(rect.x + 4, rect.yMax - 22, 20, 20), "🌷", flowerStyle);
        GUI.Label(new Rect(rect.xMax - 24, rect.yMax - 22, 20, 20), "🌻", flowerStyle);

        var iconRect = new Rect(rect.x + 12, rect.y + 10, 30, 30);
        GUI.Label(iconRect, "🌳", new GUIStyle { fontSize = 24, alignment = TextAnchor.MiddleCenter });
    }

    protected override void DrawTitle(Rect rect)
    {
        var titleStyle = new GUIStyle(EditorStyles.boldLabel) { fontSize = 14 };
        titleStyle.normal.textColor = Color.white;
        var titleRect = new Rect(rect.x + 50, rect.y + 8, rect.width - 120, 20);
        GUI.Label(titleRect, _name, titleStyle);
    }

    protected override void DrawStatusBadge(Rect rect)
    {
        var nodeCount = context.Tree.Nodes?.Count(n => n != null) ?? 0;
        var idCount = context.Tree.IDs?.Count ?? 0;
        var subtitleStyle = new GUIStyle(EditorStyles.miniLabel);
        subtitleStyle.normal.textColor = new Color(0.7f, 0.7f, 0.7f);

        var subtitleRect = new Rect(rect.x + 50, rect.y + 26, rect.width - 120, 16);
        GUI.Label(subtitleRect, $"{nodeCount} nodes • {idCount} IDs", subtitleStyle);

        var badgeRect = new Rect(rect.xMax - 70, rect.y + 15, 60, 20);
        var isValid = nodeCount > 0 && context.Tree.Nodes.All(n => n == null || !string.IsNullOrEmpty(n.ID.Value));
        EditorDrawUtils.DrawStatusBadge(badgeRect, isValid ? "Valid" : "Issues", isValid ? EditorColors.SuccessColor : EditorColors.WarningColor);
    }

    protected override void DrawFooterButtons()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("🌸 Flowers! 🌸", GUILayout.Width(100), GUILayout.Height(24)))
        {
            var rect = GUILayoutUtility.GetLastRect();
            _anim.Spawn(new Vector2(rect.center.x, rect.center.y), 15);
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    protected override void DrawFooterText()
    {
        GUILayout.Space(4);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        var style = new GUIStyle(EditorStyles.centeredGreyMiniLabel) { fontSize = 9 };
        GUILayout.Label("Node Tree Editor v1.0", style);

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        UpdateAndDrawFlowers();
    }

    private void UpdateAndDrawFlowers()
    {
        _anim.UpdateAndDraw_flowers(lastUpdateTime);
    }
}
