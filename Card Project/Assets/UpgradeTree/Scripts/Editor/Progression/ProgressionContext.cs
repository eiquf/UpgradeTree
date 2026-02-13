//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;

namespace Eiquif.UpgradeTree.Runtime
{
    public class ProgressionContext : ContextSystem
    {
        public ProgressionProviderSO So { get; }
        public SerializedObject SerializedObject { get; }

        public ProgressionContext(ProgressionProviderSO so, SerializedObject serializedObject)
        {
            So = so;
            SerializedObject = serializedObject;
        }
    }
}