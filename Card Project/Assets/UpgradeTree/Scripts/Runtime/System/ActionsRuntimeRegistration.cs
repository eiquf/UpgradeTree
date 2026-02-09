//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System;

namespace Eiquif.UpgradeTree.Runtime
{
    public class ActionsRuntimeRegistration : NodeTreeRuntimeSystem
    {
        private readonly ActionRegistry _registry = new();

        public ActionsRuntimeRegistration(NodeTree tree) : base(tree) { }

        public override void Execute()
        {
            if (Tree == null) return;
        }

        public void Subscribe(string id, Action<SkillSO> callback) => _registry.Subscribe(id, callback);
        public void Unsubscribe(string id, Action<SkillSO> callback) => _registry.Unsubscribe(id, callback);

        public void OnNodeClicked(Node node)
        {
            _registry.Invoke(node);
        }
    }
}