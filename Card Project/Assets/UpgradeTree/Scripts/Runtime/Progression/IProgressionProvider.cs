//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
/// <summary>
/// An interface for your Save/Load or Global State system 
/// so the UI doesn't depend on a specific "Player" class.
/// </summary>
public interface IProgressionProvider
{
    bool IsNodeUnlocked(NodeID id);
    int GetCurrentCurrency();
    int GetPlayerLevel();
}
public interface IProgressionWriter
{
    void UnlockNode(Node node);
    void AddCurrency(int amount);
    void ResetProgression();
}
