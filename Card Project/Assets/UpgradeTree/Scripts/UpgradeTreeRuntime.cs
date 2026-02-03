using System;
using UnityEngine;

namespace Eiquif.UpgradeTree
{
    public class UpgradeTreeRuntime : MonoBehaviour
    {
        [SerializeField] private GameObject _nodeUIPrefab;
        [SerializeField] private RectTransform _container;

        [SerializeField] private NodeTree _tree;

        private ActionsRuntimeRegistration _reg;
        private UpgradeTreeDisplay _display;
        private void OnEnable()
        {
            Initialization();
        }
        private void Start()
        {
            _reg.Execute();
            _display.Execute();

        }
        private void FixedUpdate()
        {
            _reg.OnNodeClicked(_tree.Nodes[0]);

        }
        private void Initialization()
        {
            _reg = new(_tree);
            _display = new(_tree, _nodeUIPrefab, _container);
        }

        public void Subscribe(string eventId, Action<Node> callback) => _reg.Subscribe(eventId, callback);
        public void Unsubscribe(string eventId, Action<Node> callback) => _reg.Subscribe(eventId, callback);
    }
}

public class ActionsRuntimeRegistration : UpgradeTreeRuntimeSystem
{
    private readonly ActionRegistry _registry = new();

    public ActionsRuntimeRegistration(NodeTree tree) : base(tree) { }

    public override void Execute()
    {
        if (Tree == null) return;
        Debug.Log("Система событий инициализирована");
    }

    public void Subscribe(string id, Action<Node> callback) => _registry.Subscribe(id, callback);
    public void Unsubscribe(string id, Action<Node> callback) => _registry.Unsubscribe(id, callback);

    public void OnNodeClicked(Node node)
    {
        _registry.Invoke(node.ID.Value, node);
    }
}
