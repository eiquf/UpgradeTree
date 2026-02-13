//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class ProgressionOthersSection : Section
    {
        private readonly ProgressionContext _ctx;
        private readonly SerializedObject _so;

        private bool _isExpanded;
        public ProgressionOthersSection(ContextSystem ctx) : base(ctx)
        {
            _ctx = (ProgressionContext)ctx;
            _so = _ctx.SerializedObject;
        }

        public override void Draw()
        {
            CollapsibleSection.Draw(
                "Other",
                "📚",
                ref _isExpanded,
                EditorColors.SecondaryColor,
                DrawContent
            );
        }
        #region Content
        private void DrawContent()
        {
            GUILayout.Space(8);

            _so.Update();

            SerializedProperty property = _so.GetIterator();
            bool enterChildren = true;

            while (property.NextVisible(enterChildren))
            {
                enterChildren = false;

                if (property.name == "m_Script")
                    continue;

                EditorGUILayout.PropertyField(property, true);
            }

            _so.ApplyModifiedProperties();
        }

        #endregion
    }
}