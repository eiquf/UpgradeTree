//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEngine;

public class NodeTreeRuntime : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private NodeTree _tree;

    [Header("UI")]
    [SerializeField] private GameObject _nodeUIPrefab;
    [SerializeField] private RectTransform _container;

    [Header("Default SO Logic (Optional)")]
    [SerializeField] private ProgressionProviderSO _progressionSO;
    [SerializeField] private UnlockConditionSO _conditionSO;

    private IProgressionProvider _provider;
    private UpgradeUnlockService _unlockService;

    private void Awake()
    {
        if (_progressionSO == null)
        {
            Debug.LogWarning("No Progression SO assigned. Creating default runtime progression.");
            _progressionSO = ScriptableObject.CreateInstance<DefaultProgressionSO>();
        }

        if (_conditionSO == null)
        {
            Debug.LogWarning("No Condition SO assigned. Creating default condition.");
            _conditionSO = ScriptableObject.CreateInstance<DefaultUnlockConditionSO>();
        }

        _provider = _progressionSO;

        if (_progressionSO is IProgressionWriter writer)
        {
            _unlockService = new UpgradeUnlockService(_provider, writer);
        }
        else
        {
            Debug.LogError("Progression SO must implement IProgressionWriter!");
        }
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