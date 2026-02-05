//***************************************************************************************
// Writer: Eiquif
// Last Updated: January 2026
//***************************************************************************************

using UnityEditor;

namespace Eiquif.UpgradeTree.Editor
{
    public class NodeRequirementsSection
    {
        private readonly SerializedProperty _cost;

        private readonly SerializedProperty _maxLevel;
        private readonly SerializedProperty _unlockIfParentMax;

        private readonly SerializedProperty _stats;

        private readonly SerializedProperty _icon;
        private readonly SerializedProperty _pos;
        private bool _showProperty;

        public NodeRequirementsSection(SerializedObject so)
        {
            _cost = so.FindProperty(NodePropertiesNames.Cost);
            _maxLevel = so.FindProperty(NodePropertiesNames.LevelUnlock);
            _unlockIfParentMax = so.FindProperty(NodePropertiesNames.UnlockIfParentMax);
            _stats = so.FindProperty(NodePropertiesNames.Stats);
            _icon = so.FindProperty(NodePropertiesNames.Icon);
            _pos = so.FindProperty(NodePropertiesNames.Position);
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
                    EditorGUILayout.PropertyField(_unlockIfParentMax);

                    EditorGUILayout.PropertyField(_stats);

                    EditorGUILayout.PropertyField(_icon);
                    EditorGUILayout.PropertyField(_pos);
                }
            );
        }
    }
}