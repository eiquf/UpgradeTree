using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class NodeFooterButtonsElement : IElement<NodeContext>
    {
        private readonly EditorFlowerAnimation _anim = new();

        public void Execute(NodeContext ctx)
        {
            if (ctx == null) return;

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("🌸 Flowers! 🌸", GUILayout.Width(100), GUILayout.Height(24)))
            {
                var rect = GUILayoutUtility.GetLastRect();
                _anim.Spawn(rect.center, 15);
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            _anim.UpdateAndDrawFlowers(ctx.LastUpdateTime);
        }
    }
}