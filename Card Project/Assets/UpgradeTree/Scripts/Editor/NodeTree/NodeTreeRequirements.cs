//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class NodeTreeRequirements : Section
    {
        private readonly NodeTreeContext _ctx;
        private bool _showSection;
        private readonly SerializedProperty _unlockCondition;
        private readonly SerializedProperty _progression;

        public NodeTreeRequirements(ContextSystem ctx) : base(ctx)
        {
            _ctx = ctx as NodeTreeContext;
            _progression = _ctx.SerializedObject.FindProperty(NodeTreePropertiesNames.Progression);
            _unlockCondition = _ctx.SerializedObject.FindProperty(NodeTreePropertiesNames.UnlockCondition);
        }

        public override void Draw()
        {
            CollapsibleSection.Draw(
                "Requirements",
                "❗",
                ref _showSection,
                EditorColors.SecondaryColor,
                () =>
                {
                    GUILayout.Space(4);
                    DrawProperties();
                }
            );
        }
        private void DrawProperties()
        {
            EditorGUILayout.PropertyField(_unlockCondition);
            EditorGUILayout.PropertyField(_progression);
        }
    }
}