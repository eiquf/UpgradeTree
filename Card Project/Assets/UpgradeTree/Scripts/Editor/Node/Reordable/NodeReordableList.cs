//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public class NodeReorderableList
    {
        private readonly ContextSystem _ctx;
        public ReorderableList List { get; }

        private readonly INodeTreeEditorService _service = new NodeTreeEditorService();

        private readonly List<IElement<NodeListElementContext>> _elements = new()
        {
            new NodeBackgroundElement(),
            new NodeStateIndicatorElement(),
            new NodeReferenceFieldElement(),
            new NodeIdDropdownElement()
        };

        public NodeReorderableList(
            SerializedObject so,
            SerializedProperty property,
            string header,
            Color badgeColor,
            ContextSystem ctx)
        {
            _ctx = ctx;

            List = new ReorderableList(so, property, true, true, true, true)
            {
                elementHeight = 24,

                drawHeaderCallback = rect =>
                {
                    EditorGUI.LabelField(
                        new Rect(rect.x, rect.y, rect.width - 60, rect.height),
                        header,
                        EditorStyles.boldLabel
                    );

                    EditorBadges.DrawCountBadge(
                        new Rect(rect.xMax - 55, rect.y, 55, rect.height),
                        property.arraySize,
                        badgeColor
                    );
                },

                drawElementCallback = (rect, index, _, _) =>
                    DrawElement(rect, property, index),

                onAddCallback = list =>
                {
                    if (_ctx.Tree == null) return;

                    EditorApplication.delayCall += () =>
                    {
                        var node = _service.CreateNode(_ctx.Tree);

                        list.serializedProperty.arraySize++;
                        list.serializedProperty
                            .GetArrayElementAtIndex(list.serializedProperty.arraySize - 1)
                            .objectReferenceValue = node;

                        list.serializedProperty.serializedObject.ApplyModifiedProperties();
                    };
                },

                onRemoveCallback = list =>
                {
                    if (_ctx.Tree == null) return;

                    int index = list.index;
                    if (index < 0 || index >= list.count) return;

                    var element = list.serializedProperty.GetArrayElementAtIndex(index);
                    var node = element.objectReferenceValue as Node;
                    if (node == null) return;

                    if (!EditorUtility.DisplayDialog(
                        "Delete node?",
                        $"Delete node '{node.name}'?\n\nThis action can be undone.",
                        "Delete",
                        "Cancel"))
                        return;

                    EditorApplication.delayCall += () =>
                    {
                        _service.RemoveNode(_ctx.Tree, node);
                        list.serializedProperty.serializedObject.Update();
                    };
                }
            };
        }

        protected virtual void DrawElement(Rect rect, SerializedProperty list, int index)
        {
            var element = list.GetArrayElementAtIndex(index);

            rect.y += 2;
            rect.height = EditorGUIUtility.singleLineHeight;

            var node = element.objectReferenceValue as Node;

            var ctx = new NodeListElementContext
            {
                Rect = rect,
                Property = element,
                Node = node,
                Index = index,
                Ctx = _ctx
            };

            foreach (var e in _elements)
                e.Execute(ctx);
        }
    }
}