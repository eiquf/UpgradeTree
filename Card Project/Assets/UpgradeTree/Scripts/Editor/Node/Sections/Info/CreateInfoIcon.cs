//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public class CreateInfoIcon : IElement
    {
        private readonly SerializedObject _so;
        private readonly SerializedProperty _icon;

        public CreateInfoIcon(SerializedObject so)
        {
            _so = so;
            _icon = so.FindProperty(NodePropertiesNames.Icon);
        }
        public void Execute()
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