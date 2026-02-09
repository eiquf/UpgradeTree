using System;
using UnityEngine;

namespace Eiquif.UpgradeTree.Runtime
{
    public class NodeTreeRuntime : MonoBehaviour
    {
        [SerializeField] private GameObject _nodeUIPrefab;
        [SerializeField] private RectTransform _container;

        [SerializeField] private NodeTree _tree;

        private ActionsRuntimeRegistration _reg;
        private NodeTreeDisplay _display;


        [SerializeField, NodeID(nameof(_tree))]
        private string _startNodeID;
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

        public void Subscribe(string eventId, Action<SkillSO> callback) => _reg.Subscribe(eventId, callback);
        public void Unsubscribe(string eventId, Action<SkillSO> callback) => _reg.Unsubscribe(eventId, callback);
    }
}