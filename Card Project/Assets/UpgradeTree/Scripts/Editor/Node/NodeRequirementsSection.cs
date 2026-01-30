using UnityEditor;

public class NodeRequirementsSection
{
    private readonly SerializedProperty _cost;
    private readonly SerializedProperty _maxLevel;
    private readonly SerializedProperty _stats;
    private bool _showProperty;

    public NodeRequirementsSection(SerializedObject so)
    {
        _cost = so.FindProperty("cost");
        _maxLevel = so.FindProperty("maxLevel");
        _stats = so.FindProperty("stats");
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
               }
           );
    }
}
