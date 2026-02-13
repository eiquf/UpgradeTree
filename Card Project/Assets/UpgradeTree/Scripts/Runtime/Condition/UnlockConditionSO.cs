//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEngine;

namespace Eiquif.UpgradeTree.Runtime
{
    public abstract class UnlockConditionSO : ScriptableObject, INodeUnlockCondition
    {
        public abstract bool CanUnlock(Node node, IProgressionProvider progression);
        public abstract bool IsVisible(Node node, IProgressionProvider progression);
    }
}