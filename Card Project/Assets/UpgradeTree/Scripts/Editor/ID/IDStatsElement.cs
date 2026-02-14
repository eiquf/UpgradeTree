//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class IDStatsElement : IElement<NodeTreeContext>
    {
        public void Execute(NodeTreeContext ctx)
        {
            if (ctx.IDsProp == null || ctx.IDsProp.arraySize == 0) return;

            GUILayout.Space(8);

            var used = ctx.Tree.IDs.Count(id =>
                ctx.Tree.Nodes.Any(n => n != null && n.ID.Value == id));

            var unused = ctx.Tree.IDs.Count - used;

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            EditorBadges.DrawMiniStatBadge($"✓ {used} used", EditorColors.SuccessColor);

            if (unused > 0)
            {
                GUILayout.Space(8);
                EditorBadges.DrawMiniStatBadge($"○ {unused} unused", EditorColors.WarningColor);
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
    }
}