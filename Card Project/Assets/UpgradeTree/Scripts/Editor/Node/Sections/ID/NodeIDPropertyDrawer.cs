//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    [CustomPropertyDrawer(typeof(NodeIDAttribute))]
    public class NodeIDPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = (NodeIDAttribute)attribute;

            var treeProp = property.serializedObject.FindProperty(attr.TreeFieldName);
            if (treeProp?.objectReferenceValue is not NodeTree tree)
            {
                EditorGUI.LabelField(position, label.text, "Assign NodeTree");
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            Rect contentRect = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            string newValue = NodeIDPicker.Draw(
                contentRect,
                property.stringValue,
                tree.IDs
            );

            if (EditorGUI.EndChangeCheck())
            {
                property.stringValue = newValue;
            }

            EditorGUI.EndProperty();
        }
    }
}