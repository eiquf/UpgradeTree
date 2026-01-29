using UnityEditor;
using UnityEngine;

public class NodeInfoSection
{
    private readonly SerializedObject _so;

    private readonly SerializedProperty _id;
    private readonly SerializedProperty _description;
    private readonly SerializedProperty _icon;

    private SimplePropertyDrawer _idDrawer;
    private SimplePropertyDrawer _descriptionDrawer;
    private SimplePropertyDrawer _iconDrawer;

    private bool _showIDProperty = false;
    private bool _showDescriptionProperty = false;
    private bool _showIconProperty = false;

    public NodeInfoSection(SerializedObject so)
    {
        _so = so;

        _id = so.FindProperty("ID");
        _description = so.FindProperty("<Description>k__BackingField");
        _icon = so.FindProperty("<Icon>k__BackingField");
    }

    public void Draw(bool showArrow = true)
    {
        if (_id != null)
        {
            _idDrawer = new SimplePropertyDrawer(
                _so,
                _id,
                "ID",
                EditorColors.SecondaryColor,
                "🆔"
            );
            CollapsibleSection.DrawProperty(
                "ID",
                "🆔",
                ref _showIDProperty,
                EditorColors.SecondaryColor,
                () => { _idDrawer.Draw(); }
            );
        }

        if (_description != null)
        {
            _descriptionDrawer = new SimplePropertyDrawer(
                _so,
                _description,
                "Description",
                EditorColors.PrimaryColor,
                "📚"
            );
            CollapsibleSection.Draw(
                "Description",
                "📚",
                ref _showDescriptionProperty,
                EditorColors.PrimaryColor,
                () => { _descriptionDrawer.Draw(); },
                showArrow
            );
        }

        if (_icon != null)
        {
            _iconDrawer = new SimplePropertyDrawer(
                _so,
                _icon,
                "Icon",
                EditorColors.SuccessBgLight,
                "🖼"
            );
            CollapsibleSection.Draw(
                "Icon",
                "🖼",
                ref _showIconProperty,
                EditorColors.SuccessBgLight,
                () => { _iconDrawer.Draw(); },
                showArrow
            );
            DrawIconPreview();
        }

        EditorGUILayout.Space(4);
    }

    private void DrawIconPreview()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.PropertyField(_icon);

        if (_icon.objectReferenceValue != null)
        {
            var rect = GUILayoutUtility.GetRect(48, 48);
            GUI.DrawTexture(rect, ((Sprite)_icon.objectReferenceValue).texture);
        }

        EditorGUILayout.EndHorizontal();
    }
}
