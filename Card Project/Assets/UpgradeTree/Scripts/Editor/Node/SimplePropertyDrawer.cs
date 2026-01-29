using UnityEditor;
using UnityEngine;

public class SimplePropertyDrawer
{
    //private readonly SerializedObject _so;
    private readonly SerializedProperty _property;
    private bool _showProperty;

    private readonly string _header;
    private readonly string _emojiIcon;
    private readonly Color _badgeColor;

    public SimplePropertyDrawer(SerializedObject so, SerializedProperty property, string header, Color badgeColor, string emojiIcon)
    {
        //_so = so;
        _property = property;
        _header = header;
        _badgeColor = badgeColor;
        _emojiIcon = emojiIcon;
    }

    public void Draw()
    {
        CollapsibleSection.Draw(
            _header,
            _emojiIcon,
            ref _showProperty,
            _badgeColor,
            () => { 
            }
        );

        if (_showProperty)
        {
            EditorGUILayout.BeginVertical(EditorStyleCache.CardStyle);

            EditorGUILayout.LabelField("Property Info", EditorStyleCache.HeaderStyle);

            if (_property.propertyType == SerializedPropertyType.Integer)
                EditorGUILayout.PropertyField(_property, new GUIContent("Integer Value"));
            else if (_property.propertyType == SerializedPropertyType.Float)
                EditorGUILayout.PropertyField(_property, new GUIContent("Float Value"));
            else if (_property.propertyType == SerializedPropertyType.Boolean)
                EditorGUILayout.PropertyField(_property, new GUIContent("Boolean Value"));
            else
                EditorGUILayout.PropertyField(_property);

            EditorGUILayout.EndVertical();
        }
    }

}