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

        [Header("UI Settings")]
        [SerializeField] private GameObject _nodeUIPrefab;
        [SerializeField] private RectTransform _container;

        [Header("Injectable Logic")]
        [Tooltip("Drop here a progression SO")]
        [SerializeField] private ProgressionProviderSO _progressionAsset;

        [Tooltip("Drop here condition SO")]
        [SerializeField] private UnlockConditionSO _conditionAsset;

        private NodeTreeDisplay _display;

        private void OnEnable()
        {
            if (_progressionAsset == null || _conditionAsset == null)
            {
                Debug.LogError("NodeTreeRuntime: Missing Logic Assets!");
                return;
            }

            _display = new(_tree, _nodeUIPrefab, _container, _conditionAsset, _progressionAsset);
        }

        private void Start() => _display?.Execute();
    }
}