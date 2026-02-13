using Eiquif.UpgradeTree.Runtime;
using UnityEngine;

[CreateAssetMenu(fileName = "TestGameCondition", menuName = "UpgradeTree/Condition/TestGameCondition")]
public class TestGameCondition : UnlockConditionSO
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