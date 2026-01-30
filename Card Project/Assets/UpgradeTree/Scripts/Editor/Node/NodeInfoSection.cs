using UnityEditor;
using UnityEngine;

internal sealed class NodeInfoSection
{
    private readonly SerializedObject _so;
    private readonly NodeContext _ctx;

    private readonly SerializedProperty _id;
    private readonly SerializedProperty _description;
    private readonly SerializedProperty _icon;

    private readonly SerializedProperty _idValue;

    private readonly NodeReorderableList _idList;

    private bool _isExpanded;

    public NodeInfoSection(SerializedObject so, NodeContext ctx)
    {
        _so = so;
        _ctx = ctx;

        _id = so.FindProperty("id");
        _description = so.FindProperty("description");
        _icon = so.FindProperty("icon");

        _idValue = _id?.FindPropertyRelative("Value");

    }

    public void Draw(bool showArrow = true)
    {
        if (_id == null) return;

        CollapsibleSection.Draw(
            "Information",
            "\U0001f978",
            ref _isExpanded,
            EditorColors.SecondaryColor,
            DrawContent,
            showArrow
        );

       
    }

    private void DrawContent()
    {
        var idRect = EditorGUILayout.GetControlRect();

        _ctx.Node.ID.Value = NodeIDField.Draw(
            idRect,
            _ctx.Node.ID.Value,
            _ctx.Node,
            _ctx
        );

        EditorUtility.SetDirty(_ctx.Node);

        EditorGUILayout.PropertyField(_description);
        EditorGUILayout.PropertyField(_icon);

        DrawIconPreview();
    }

    private void DrawIconPreview()
    {
        if (_icon.objectReferenceValue is not Sprite sprite)
            return;

        var rect = GUILayoutUtility.GetRect(48, 48);
        GUI.DrawTexture(rect, sprite.texture);
    }
}