//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;

public class DefaultUnlockCondition : INodeUnlockCondition
{
    public bool IsVisible(Node node, IProgressionProvider progression)
    {
        if (node.PrerequisiteNodes.Count == 0) return true;

        foreach (var req in node.PrerequisiteNodes)
        {
            if (progression.IsNodeUnlocked(req.ID)) return true;
        }
        return false;
    }

    public bool CanUnlock(Node node, IProgressionProvider progression)
    {
        foreach (var req in node.PrerequisiteNodes)
        {
            if (!progression.IsNodeUnlocked(req.ID)) return false;
        }

        if (progression.GetCurrentCurrency() < node.Cost) return false;

        if (progression.GetPlayerLevel() < node.ParentUnlockLevel) return false;

        return true;
    }
}