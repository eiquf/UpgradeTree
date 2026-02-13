using Eiquif.UpgradeTree.Runtime;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestGameProgression", menuName = "UpgradeTree/Progression/Default Progression")]
public class TestGameProgression : ProgressionProviderSO
{
    [SerializeField] private int _startCurrency = 100;

    private int _currency;
    private readonly HashSet<string> _unlockedNodes = new();

    private void OnEnable()
    {
        Load();
        SyncFromSave();
    }

    private void SyncFromSave()
    {
        if (_data == null)
        {
            ResetRuntimeState();
            return;
        }

        _currency = _data.SavedCurrency == 0 ? _startCurrency : _data.SavedCurrency;

        _unlockedNodes.Clear();
        foreach (var id in _data.UnlockedNodeIds)
        {
            _unlockedNodes.Add(id);
        }
    }

    private void SyncToSave()
    {
        _data.SavedCurrency = _currency;
        _data.UnlockedNodeIds = new List<string>(_unlockedNodes);
        Save();
    }

    private void ResetRuntimeState()
    {
        _currency = _startCurrency;
        _unlockedNodes.Clear();
    }

    public override int GetCurrentCurrency() => _currency;

    public override void AddCurrency(int amount)
    {
        _currency += amount;
        SyncToSave();
    }

    public override int GetPlayerLevel() => 1;

    public override bool IsNodeUnlocked(NodeID id)
    {
        return _unlockedNodes.Contains(id.Value); 
    }

    public override void UnlockNode(Node node)
    {
        if (_unlockedNodes.Contains(node.ID.Value))
            return;

        _unlockedNodes.Add(node.ID.Value);
        _currency -= node.Cost;

        SyncToSave();
    }

    public override void ResetProgression()
    {
        ResetRuntimeState();

        _data.SavedCurrency = _currency;
        _data.UnlockedNodeIds = new List<string>(_unlockedNodes);
        Save();
        Debug.Log("Progression cleared on disk and in memory.");
    }
}