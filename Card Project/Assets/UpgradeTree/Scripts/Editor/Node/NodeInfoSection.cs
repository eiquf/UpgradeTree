using UnityEditor;
using UnityEngine;
using Eiquif.UpgradeTree.Runtime;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class NodeInfoSection
    {
        private readonly SerializedObject _so;
        private readonly NodeContext _ctx;

        private readonly SerializedProperty _id;
        private readonly SerializedProperty _description;
        private readonly SerializedProperty _icon;
        private readonly SerializedProperty _activatedStatus;

        private bool _isExpanded;

        public NodeInfoSection(SerializedObject so, NodeContext ctx)
        {
            _so = so;
            _ctx = ctx;

            _id = so.FindProperty("id");
            _description = so.FindProperty("description");
            _icon = so.FindProperty("icon");
            _icon = so.FindProperty("icon");
            _activatedStatus = so.FindProperty("activated");
        }

        public void Draw(bool showArrow = true)
        {
            if (_id == null) return;

            CollapsibleSection.Draw(
                "Information",
                "\U0001f978",
                ref _isExpanded,
                EditorColors.SecondaryColor,
                DrawContent,
                showArrow
            );
        }

        private void DrawContent()
        {
            DrawID();
            EditorGUILayout.PropertyField(_activatedStatus);

            GUILayout.Space(8);
            EditorGUILayout.PropertyField(_description);

            GUILayout.Space(8);
            DrawIconPreview();
        }
        private void DrawID()
        {
            GUILayout.Label("ID", EditorStyles.boldLabel);
            var idRect = EditorGUILayout.GetControlRect();

            _ctx.Node.ID.Value = NodeIDField.Draw(
                idRect,
                _ctx.Node.ID.Value,
                _ctx.Node,
                _ctx
            );
            EditorUtility.SetDirty(_ctx.Node);
        }
        private void DrawIconPreview()
        {
            GUILayout.Label("Icon", EditorStyles.boldLabel);
            Rect rect = GUILayoutUtility.GetRect(64, 64, GUILayout.ExpandWidth(false));

            EditorGUI.BeginChangeCheck();

            EditorGUI.ObjectField(
                rect,
                _icon,
                GUIContent.none
            );

            if (EditorGUI.EndChangeCheck())
            {
                _so.ApplyModifiedProperties();
            }

            if (_icon.objectReferenceValue is Sprite sprite)
            {
                var tex = AssetPreview.GetAssetPreview(sprite);
                if (tex != null)
                    GUI.DrawTexture(rect, tex, ScaleMode.ScaleToFit);
            }

            GUI.Box(rect, GUIContent.none);
        }
    }
}