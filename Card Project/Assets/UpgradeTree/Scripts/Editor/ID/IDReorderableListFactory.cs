//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public static class IDReorderableListFactory
    {
        public static ReorderableList Create(NodeTreeContext ctx)
        {
            var list = new ReorderableList(ctx.SerializedObject, ctx.IDsProp, true, true, true, true)
            {
                drawHeaderCallback = rect =>
            {
                var labelRect = new Rect(rect.x, rect.y, rect.width - 60, rect.height);
                var countRect = new Rect(rect.xMax - 55, rect.y, 55, rect.height);

                EditorGUI.LabelField(labelRect, "Registered IDs", EditorStyles.boldLabel);
                EditorBadges.DrawCountBadge(countRect, ctx.IDsProp.arraySize, EditorColors.PrimaryColor);
            },

                drawElementCallback = (rect, index, active, focused) =>
                {
                    var element = ctx.IDsProp.GetArrayElementAtIndex(index);
                    var id = element.stringValue;

                    var usageCount = ctx.Tree?.Nodes?.Count(n => n != null && n.ID.Value == id) ?? 0;

                    rect.y += 2;
                    rect.height = EditorGUIUtility.singleLineHeight;

                    var bgColor = usageCount == 0
                        ? EditorColors.WarningBgLight
                        : EditorColors.SuccessBgLight;

                    EditorGUI.DrawRect(
                        new Rect(rect.x - 4, rect.y - 2, rect.width + 8, rect.height + 4),
                        bgColor
                    );

                    EditorGUI.PropertyField(
                        new Rect(rect.x + 4, rect.y, rect.width - 70, rect.height),
                        element,
                        GUIContent.none
                    );

                    EditorGUI.LabelField(
                        new Rect(rect.xMax - 60, rect.y, 55, rect.height),
                        $"×{usageCount} used",
                        EditorStyles.miniLabel
                    );
                },

                onAddCallback = l =>
                {
                    var index = l.serializedProperty.arraySize;
                    l.serializedProperty.InsertArrayElementAtIndex(index);
                    l.serializedProperty.GetArrayElementAtIndex(index).stringValue = $"NewID_{index}";
                },

                onRemoveCallback = l =>
                {
                    ReorderableList.defaultBehaviours.DoRemoveButton(l);
                },

                elementHeight = 24
            };
            return list;
        }
    }
}