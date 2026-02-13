using Eiquif.UpgradeTree.Runtime;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultProgression", menuName = "UpgradeTree/Progression/Default Progression")]
public class DefaultProgressionSO : ProgressionProviderSO
{
    [SerializeField] private int _startCurrency = 100;

    private int _currency;
    private HashSet<NodeID> _unlocked = new();

    private void OnEnable()
    {
        _currency = _startCurrency;
        _unlocked.Clear();
    }

    public override bool IsNodeUnlocked(NodeID id)
        => _unlocked.Contains(id);

    public override int GetCurrentCurrency()
        => _currency;

    public override int GetPlayerLevel()
        => 1;

    public override void UnlockNode(Node node)
    {
        if (_unlocked.Contains(node.ID))
            return;

        _unlocked.Add(node.ID);
        _currency -= node.Cost;
    }
    public override void ResetProgression()
    {
        _unlocked.Clear();
        _currency = _startCurrency;
    }

    public override void AddCurrency(int amount) => _currency += amount;
}

[CreateAssetMenu(fileName = "DefaultCondition", menuName = "UpgradeTree/Condition/Default Condition")]
public class DefaultUnlockConditionSO : UnlockConditionSO
{
    public override bool CanUnlock(Node node, IProgressionProvider progression)
    {
        if (progression.GetCurrentCurrency() < node.Cost)
            return false;

        foreach (var parent in node.PrerequisiteNodes)
        {
            if (!progression.IsNodeUnlocked(parent.ID))
                return false;
        }

        return true;
    }

    public override bool IsVisible(Node node, IProgressionProvider progression)
    {
        foreach (var parent in node.PrerequisiteNodes)
        {
            if (!progression.IsNodeUnlocked(parent.ID))
                return false;
        }

        return true;
    }
}