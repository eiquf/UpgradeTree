using Eiquif.UpgradeTree.Runtime;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public class CreateValidationRemoveDupButton : IElement<ValidationCtx>
    {
        public void Execute(ValidationCtx ctx)
        {
            if (!GUILayout.Button("👯 Remove Duplicates", GUILayout.Height(24)))
                return;

            Undo.RecordObject(ctx.UndoTarget, "Remove Duplicate Nodes");

            var seenIds = new HashSet<string>();
            var uniqueList = new List<Node>();
            int removed = 0;

            foreach (var node in ctx.Nodes)
            {
                if (node == null)
                {
                    uniqueList.Add(null);
                    continue;
                }

                string id = node.ID.Value;
                if (string.IsNullOrEmpty(id))
                {
                    uniqueList.Add(node);
                    continue;
                }

                if (seenIds.Add(id))
                {
                    uniqueList.Add(node);
                }
                else
                {
                    removed++;
                }
            }

            ctx.Nodes.Clear();
            ctx.Nodes.AddRange(uniqueList);

            ctx.ApplyChanges?.Invoke(ctx.UndoTarget, removed);
        }
    }
}