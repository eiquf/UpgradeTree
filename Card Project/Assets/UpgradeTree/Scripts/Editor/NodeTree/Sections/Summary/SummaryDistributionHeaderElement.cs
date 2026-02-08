using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class SummaryDistributionHeaderElement : IElement
    {
        public void Execute()
        {
            GUILayout.Space(8);
            EditorGUILayout.LabelField("ID Distribution", EditorStyles.boldLabel);
            GUILayout.Space(4);
        }
    }

}