//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;

public interface INodeUnlockCondition
{
    /// <summary>
    /// Determines if a node is currently interactable/purchasable.
    /// </summary>
    bool CanUnlock(Node node, IProgressionProvider progression);

    /// <summary>
    /// Determines if the node should even be visible on the UI.
    /// </summary>
    bool IsVisible(Node node, IProgressionProvider progression);
}