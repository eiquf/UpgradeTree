using UnityEditor;

public class NodeRequirementsSection
{
    private readonly SerializedProperty _cost;
    private readonly SerializedProperty _maxLevel;
    private readonly SerializedProperty _stats;
    private readonly SerializedProperty _icon;
    private readonly SerializedProperty _pos;
    private bool _showProperty;

    public NodeRequirementsSection(SerializedObject so)
    {
        _cost = so.FindProperty("cost");
        _maxLevel = so.FindProperty("maxLevel");
        _stats = so.FindProperty("stats");
        _icon = so.FindProperty("icon");
        _pos = so.FindProperty("position");
    }

    public void Draw()
    {
        CollapsibleSection.Draw(
               "Requirements",
               "📚",
               ref _showProperty,
               EditorColors.SecondaryColor,
               () =>
               {
                   EditorGUILayout.PropertyField(_cost);
                   EditorGUILayout.PropertyField(_maxLevel);
                   EditorGUILayout.PropertyField(_stats);
                   EditorGUILayout.PropertyField(_icon);
                   EditorGUILayout.PropertyField(_pos);
               }
           );
    }
}
