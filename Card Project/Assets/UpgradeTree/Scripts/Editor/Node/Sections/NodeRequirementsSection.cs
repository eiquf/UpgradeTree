//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Codice.Client.Common.GameUI;
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;

namespace Eiquif.UpgradeTree.Editor
{
    public class NodeRequirementsSection
    {
        #region Serialized
        private readonly SerializedProperty _cost;

        private readonly SerializedProperty _maxLevel;
        private readonly SerializedProperty _unlockIfParentMax;

        private readonly SerializedProperty _stats;
        #endregion

        private bool _showProperty;

        public NodeRequirementsSection(SerializedObject so)
        {
            _cost = so.FindProperty(NodePropertiesNames.Cost);
            _maxLevel = so.FindProperty(NodePropertiesNames.LevelUnlock);
            _unlockIfParentMax = so.FindProperty(NodePropertiesNames.UnlockIfParentMax);
            _stats = so.FindProperty(NodePropertiesNames.Stats);
        }

        public void Draw()
        {
            CollapsibleSection.Draw(
                "Requirements",
                "📚",
                ref _showProperty,
                EditorColors.SecondaryColor,
                DrawContent
            );
        }
        private void DrawContent()
        {
            EditorGUILayout.PropertyField(_cost);
            EditorGUILayout.PropertyField(_maxLevel);
            EditorGUILayout.PropertyField(_unlockIfParentMax);
            EditorGUILayout.PropertyField(_stats);
        }
    }
}