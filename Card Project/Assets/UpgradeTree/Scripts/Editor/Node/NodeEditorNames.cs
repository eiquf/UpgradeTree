using UnityEditor;
using UnityEngine;
using System.Linq;
/// <summary>
/// Custom editor names for the <see cref="NodeTree"/> editor interface.
/// This class handles drawing the background, borders, icons, title, status badges, footer buttons,
/// and footer text for the node tree editor, as well as managing flower animations.
/// </summary>
public class NodeEditorNames : EditorNames
{
    private readonly NodeContext _ctx;

    private readonly string _name;
    private readonly EditorFlowerAnimation _anim = new();

    /// <param name="context">The <see cref="NodeContext"/> providing data for the node tree editor.</param>
    /// <param name="name">The name of the node tree.</param>
    /// <param name="lastUpdateTime">The last time the editor was updated.</param>
    public NodeEditorNames(ContextSystem context, string name)
        : base(context)
    {
        _name = name;
        _ctx = (NodeContext)context;
    }

    protected override void DrawBackground(Rect rect) =>
        EditorDrawUtils.DrawGradientRect(rect, new Color(0.2f, 0.25f, 0.35f), new Color(0.15f, 0.18f, 0.25f));
    protected override void DrawBorders(Rect rect) => EditorDrawUtils.DrawBorder(rect, EditorColors.PrimaryColor, 2);

    /// <summary>
    /// Draws the icons at the corners of the editor header.
    /// </summary>
    /// <param name="rect">The rectangle where the icons should be drawn.</param>
    protected override void DrawIcons(Rect rect)
    {
        var flowerStyle = new GUIStyle { fontSize = 16, alignment = TextAnchor.MiddleCenter };
        GUI.Label(new Rect(rect.x + 4, rect.y + 2, 20, 20), "🎀", flowerStyle);
        GUI.Label(new Rect(rect.xMax - 24, rect.y + 2, 20, 20), "✨", flowerStyle);
        GUI.Label(new Rect(rect.x + 4, rect.yMax - 22, 20, 20), "🔮", flowerStyle);
        GUI.Label(new Rect(rect.xMax - 24, rect.yMax - 22, 20, 20), "🌴", flowerStyle);

        var iconRect = new Rect(rect.x + 12, rect.y + 10, 30, 30);
        GUI.Label(iconRect, "🎁", new GUIStyle { fontSize = 24, alignment = TextAnchor.MiddleCenter });
    }
    protected override void DrawTitle(Rect rect)
    {
        var titleStyle = new GUIStyle(EditorStyles.boldLabel) { fontSize = 14 };
        titleStyle.normal.textColor = Color.white;
        var titleRect = new Rect(rect.x + 50, rect.y + 8, rect.width - 120, 20);
        GUI.Label(titleRect, _name, titleStyle);
    }

    /// <summary>
    /// Draws the status badge with information about the number of nodes and IDs.
    /// Also indicates if there are any issues with the node tree.
    /// </summary>
    /// <param name="rect">The rectangle where the status badge should be drawn.</param>
    protected override void DrawStatusBadge(Rect rect)
    {
        var nodeNextCount = _ctx.Node.NextNodes?.Count(n => n != null) ?? 0;
        var nodePrerequisiteCount = _ctx.Node.PrerequisiteNodes?.Count ?? 0;
        var subtitleStyle = new GUIStyle(EditorStyles.miniLabel)
        {
            normal = { textColor = new Color(0.7f, 0.7f, 0.7f) }
        };

        var subtitleRect = new Rect(rect.x + 50, rect.y + 26, rect.width - 120, 16);
        GUI.Label(subtitleRect, $"{nodeNextCount} next nodes • {nodePrerequisiteCount} prerequisite nodes", subtitleStyle);

        var badgeRect = new Rect(rect.xMax - 70, rect.y + 15, 60, 20);
        var isValid = nodeNextCount > 0 && _ctx.Node.NextNodes.All(n => n == null || !string.IsNullOrEmpty(n.ID.Value));
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
        UpdateAndDrawFlowersAnim();
    }
    private void UpdateAndDrawFlowersAnim() => _anim.UpdateAndDraw_flowers(_ctx.LastUpdateTime);
}