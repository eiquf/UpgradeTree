namespace Eiquif.UpgradeTree.Runtime.Tree
{
    using System;
    using Eiquif.UpgradeTree.Runtime.Node;

    public class ActionsRuntimeRegistration : UpgradeTreeRuntimeSystem
    {
        private readonly ActionRegistry _registry = new();

        public ActionsRuntimeRegistration(NodeTree tree) : base(tree) { }

        public override void Execute()
        {
            if (Tree == null) return;
        }

        public void Subscribe(string id, Action<Node> callback) => _registry.Subscribe(id, callback);
        public void Unsubscribe(string id, Action<Node> callback) => _registry.Unsubscribe(id, callback);

        public void OnNodeClicked(Node node)
        {
            _registry.Invoke(node.ID.Value, node);
        }
    }
}


