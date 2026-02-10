//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System;
using System.Collections.Generic;

namespace Eiquif.UpgradeTree.Runtime
{
    [Serializable]
    public class UpgradeTreeSaveData
    {
        public List<string> UnlockedNodeIds = new();
        public int SavedCurrency;
    }
}
