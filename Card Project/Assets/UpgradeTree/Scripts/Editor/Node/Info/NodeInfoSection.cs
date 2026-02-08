//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class NodeInfoSection
    {
        private readonly NodeContext _ctx;

        #region Serialized
        private readonly SerializedObject _so;

        private readonly SerializedProperty _description;
        private readonly SerializedProperty _pos;
        #endregion

        private bool _isExpanded;

        private readonly IElement _drawID;
        private readonly IElement _drawIcon;
        public NodeInfoSection(SerializedObject so, NodeContext ctx)
        {
            _so = so;
            _ctx = ctx;

            _description = so.FindProperty(NodePropertiesNames.Description);
            _pos = so.FindProperty(NodePropertiesNames.Position);

            _drawID = new CreateInfoID(_ctx);
            _drawIcon = new CreateInfoIcon(_so);
        }

        public void Draw(bool showArrow = true)
        {
            CollapsibleSection.Draw(
                "Information",
                "\U0001f978",
                ref _isExpanded,
                EditorColors.SecondaryColor,
                DrawContent,
                showArrow
            );
        }
        #region Content
        private void DrawContent()
        {
            GUILayout.Space(8);
            DrawID();

            GUILayout.Space(8);
            EditorGUILayout.PropertyField(_description);

            GUILayout.Space(8);
            EditorGUILayout.PropertyField(_pos);

            GUILayout.Space(8);
            DrawIconPreview();

        }
        private void DrawID() => _drawID.Execute();
        private void DrawIconPreview() => _drawIcon.Execute();
        #endregion
    }
}