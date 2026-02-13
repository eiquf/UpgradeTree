//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System.Collections.Generic;

namespace Eiquif.UpgradeTree.Runtime
{
    [System.Serializable]
    public class UpgradeTreeSaveData
    {
        public bool Initialized;
        public int SavedCurrency;
        public List<string> UnlockedNodeIds = new();
    }
}