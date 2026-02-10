//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEngine;

namespace Eiquif.UpgradeTree.Runtime
{
    public abstract class ProgressionProviderSO : ScriptableObject, IProgressionProvider
    {
        public abstract bool IsNodeUnlocked(NodeID id);
        public abstract int GetCurrentCurrency();
        public abstract int GetPlayerLevel();
        public abstract void UnlockNode(Node node);
        public abstract void ResetProgression();
    }
}