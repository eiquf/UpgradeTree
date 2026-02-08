using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public class CreateInfoID : IElement
    {
        private readonly NodeContext _ctx;
        public CreateInfoID(NodeContext ctx) => _ctx = ctx;
        public void Execute()
        {
            GUILayout.Label("ID", EditorStyles.boldLabel);
            var idRect = EditorGUILayout.GetControlRect();

            _ctx.Node.ID.Value = NodeIDField.Draw(
                idRect,
                _ctx.Node.ID.Value,
                _ctx.Node,
                _ctx
            );
            UnityEditor.EditorUtility.SetDirty(_ctx.Node);
        }
    }
}