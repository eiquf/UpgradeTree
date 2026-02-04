namespace Eiquif.UpgradeTree.Editor.Node
{
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
            _cost = so.FindProperty("_cost");
            _maxLevel = so.FindProperty("_maxLevel");
            _stats = so.FindProperty("_stats");
            _icon = so.FindProperty("_icon");
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
}