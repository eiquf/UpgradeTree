using UnityEditor;

public class NodeRequirementsSection
{
    private readonly SerializedProperty _cost;
    private readonly SerializedProperty _maxLevel;
    private readonly SerializedProperty _stats;

    public NodeRequirementsSection(SerializedObject so)
    {
        _cost = so.FindProperty("<Cost>k__BackingField");
        _maxLevel = so.FindProperty("<MaxLevel>k__BackingField");
        _stats = so.FindProperty("<Stats>k__BackingField");
    }

    public void Draw()
    {
        EditorGUILayout.BeginVertical(EditorStyleCache.CardStyle);

        EditorGUILayout.LabelField("Requirements", EditorStyleCache.HeaderStyle);

        EditorGUILayout.PropertyField(_cost);
        EditorGUILayout.PropertyField(_maxLevel);
        EditorGUILayout.PropertyField(_stats);

        EditorGUILayout.EndVertical();
    }
}
