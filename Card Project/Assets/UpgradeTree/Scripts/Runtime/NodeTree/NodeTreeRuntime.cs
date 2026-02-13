//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEngine;

namespace Eiquif.UpgradeTree.Runtime
{
    public class NodeTreeRuntime : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private NodeTree _tree;

        [Header("UI")]
        [SerializeField] private GameObject _nodeUIPrefab;
        [SerializeField] private RectTransform _container;

        private IProgressionProvider _provider;
        private UpgradeUnlockService _unlockService;

        private void Awake()
        {
            if (_tree.ProgressionProvider == null)
                Debug.LogError("No Progression SO assigned.  Please create runtime progression.");

            if (_tree.UnlockCondition == null)
                Debug.LogError("No Condition SO assigned. Please create condition.");

            _provider = _tree.ProgressionProvider;

            if (_tree.ProgressionProvider is IProgressionWriter writer)
                _unlockService = new UpgradeUnlockService(_provider, writer);
            else
                Debug.LogError("Progression SO must implement IProgressionWriter!");
        }


        private void Start()
        {
            var display = new NodeTreeDisplay(
                _tree,
                _nodeUIPrefab,
                _container,
                _unlockService);

            display.Execute();
        }
    }
}