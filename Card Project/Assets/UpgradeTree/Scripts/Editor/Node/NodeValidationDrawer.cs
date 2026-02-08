//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class NodeValidationDrawer
    {
        private readonly EditorFlowerAnimation _anim = new();

        private readonly IElement _header = new CreateValidationHeader();
        private readonly IElement<ValidationCtx> _items = new ValidationItemsElement();
        private readonly IElement<ValidationCtx> _actions = new ValidationActionsElement();

        public void Draw(List<Node> nodes, Object undoTarget)
        {
            if (nodes == null || nodes.Count == 0)
                return;

            var ctx = BuildContext(nodes, undoTarget);

            if (ctx.NullCount == 0 &&
                ctx.NoIdCount == 0 &&
                ctx.DuplicateCount == 0)
                return;

            GUILayout.Space(8);
            EditorGUILayout.BeginVertical(EditorStyleCache.CardStyle);

            _header.Execute();
            _items.Execute(ctx);

            GUILayout.Space(8);

            _actions.Execute(ctx);

            EditorGUILayout.EndVertical();
        }

        private ValidationCtx BuildContext(List<Node> nodes, Object undoTarget)
        {
            var duplicateCount = nodes
                .Where(n => n != null)
                .Select(n => n.ID.Value)
                .Where(id => !string.IsNullOrEmpty(id))
                .GroupBy(id => id)
                .Sum(g => Mathf.Max(0, g.Count() - 1));

            return new ValidationCtx
            {
                Nodes = nodes,
                UndoTarget = undoTarget,
                NullCount = nodes.Count(n => n == null),
                NoIdCount = nodes.Count(n => n != null && string.IsNullOrEmpty(n.ID.Value)),
                DuplicateCount = duplicateCount,
                ApplyChanges = ApplyChanges
            };
        }

        private void ApplyChanges(Object undoTarget, int changeCount)
        {
            EditorUtility.SetDirty(undoTarget);

            if (changeCount <= 0)
                return;

            var rect = GUILayoutUtility.GetLastRect();
            if (rect.width > 0f)
                _anim.Spawn(rect.center, changeCount * 3);
        }
    }
}