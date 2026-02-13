//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;

public class UpgradeUnlockService
{
    private readonly IProgressionProvider _provider;
    private readonly IProgressionWriter _writer;

    public IProgressionProvider Provider => _provider;


    public UpgradeUnlockService(
        IProgressionProvider provider,
        IProgressionWriter writer)
    {
        _provider = provider;
        _writer = writer;
    }

    public bool TryUnlock(Node node)
    {
        if (!CanUnlock(node))
            return false;

        _writer.UnlockNode(node);
        ActionRegistry.Invoke(node);

        return true;
    }

    public bool CanUnlock(Node node)
    {
        if (node.Conditions == null || node.Conditions.Count == 0)
            return true;

        foreach (var condition in node.Conditions)
        {
            if (!condition.CanUnlock(node, _provider))
                return false;
        }

        return true;
    }

    public bool IsVisible(Node node)
    {
        if (node.Conditions == null || node.Conditions.Count == 0)
            return true;

        foreach (var condition in node.Conditions)
        {
            if (!condition.IsVisible(node, _provider))
                return false;
        }

        return true;
    }
}