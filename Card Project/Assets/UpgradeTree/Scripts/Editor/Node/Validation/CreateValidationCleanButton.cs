//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public class CreateValidationCleanButton : IElement<ValidationCtx>
    {
        public void Execute(ValidationCtx ctx)
        {
            if (GUILayout.Button("🧹 Clean Empty Slots", GUILayout.Height(24)))
            {
                Undo.RecordObject(ctx.UndoTarget, "Clean Null Nodes");
                int removed = ctx.Nodes.RemoveAll(n => n == null);
                ctx.ApplyChanges?.Invoke(ctx.UndoTarget, removed);
            }
        }
    }
}

