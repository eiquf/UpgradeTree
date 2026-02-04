namespace Eiquif.UpgradeTree.Runtime.Tree
{
    using System;
    using UnityEngine;
    using Runtime.Node;
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