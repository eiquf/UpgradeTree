namespace Eiquif.UpgradeTree.Editor.Node
{
    using Runtime.Node;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;
    using RuntimeNode = Runtime.Node.Node;

    public class NodeReorderableList
    {
        private readonly ContextSystem _ctx;
        public ReorderableList List { get; }

        private readonly EditorFlowerAnimation _anim = new();

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

                    EditorDrawUtils.DrawCountBadge(
                        new Rect(rect.xMax - 55, rect.y, 55, rect.height),
                        property.arraySize,
                        badgeColor
                    );
                },

                drawElementCallback = (rect, index, _, _) =>
                  DrawElement(rect, property, index),

                drawElementBackgroundCallback = DrawBackground,
            };
            List.onRemoveCallback = list =>
            {
                if (_ctx.Tree == null) return;

                int index = list.index;
                if (index < 0 || index >= list.count)
                    return;

                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                var node = element.objectReferenceValue as Node;

                if (node == null)
                    return;

                if (!EditorUtility.DisplayDialog(
                    "Delete node?",
                    $"Delete node '{node.name}'?\n\nThis action can be undone.",
                    "Delete",
                    "Cancel"))
                    return;

                _ctx.Tree.RemoveNodeSafe(node);

                list.serializedProperty.serializedObject.Update();
            };
            List.onAddCallback = list =>
            {
                if (_ctx.Tree == null)
                    return;

                var node = _ctx.Tree.CreateNode();

                list.serializedProperty.arraySize++;
                list.serializedProperty
                    .GetArrayElementAtIndex(list.serializedProperty.arraySize - 1)
                    .objectReferenceValue = node;

                list.serializedProperty.serializedObject.ApplyModifiedProperties();

                _anim?.Spawn(Event.current.mousePosition, 5);
            };
        }
        protected virtual void DrawElement(Rect rect, SerializedProperty list, int index)
        {
            var element = list.GetArrayElementAtIndex(index);
            rect.y += 2;
            rect.height = EditorGUIUtility.singleLineHeight;

            var node = element.objectReferenceValue as RuntimeNode;
            var hasId = node != null && !string.IsNullOrEmpty(node.ID.Value);

            var indicatorColor = node == null
                ? EditorColors.ErrorColor
                : hasId ? EditorColors.SuccessColor : EditorColors.WarningColor;

            var bgColor = node == null
                ? EditorColors.ErrorBgLight
                : hasId ? EditorColors.SuccessBgLight : EditorColors.WarningBgLight;

            EditorGUI.DrawRect(
                new Rect(rect.x - 4, rect.y - 2, 3, rect.height + 4),
                indicatorColor
            );

            EditorGUI.DrawRect(
                new Rect(rect.x - 1, rect.y - 2, rect.width + 2, rect.height + 4),
                bgColor
            );

            EditorGUI.PropertyField(
                new Rect(rect.x + 4, rect.y, rect.width - 130, rect.height),
                element,
                GUIContent.none
            );

            if (node != null)
            {
                var idRect = new Rect(rect.xMax - 120, rect.y, 115, rect.height);
                var buttonLabel = hasId ? node.ID.Value : "Select ID...";
                var buttonStyle = new GUIStyle(EditorStyles.popup)
                {
                    fontSize = 10
                };

                if (EditorGUI.DropdownButton(idRect, new GUIContent(buttonLabel),
                FocusType.Keyboard, buttonStyle))
                {
                    _ctx.IDMenu.Show(node);
                }
            }
        }
        protected virtual void DrawBackground(Rect rect, int index, bool active, bool focused)
        {
            if (active)
                EditorGUI.DrawRect(rect, new Color(0.4f, 0.3f, 0.6f, 0.3f));
            else if (index % 2 == 0)
                EditorGUI.DrawRect(rect, new Color(0f, 0f, 0f, 0.1f));
        }
    }
}