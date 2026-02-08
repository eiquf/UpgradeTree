//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class NodeFooterTextElement : IElement
    {
        public void Execute()
        {
            GUILayout.Space(4);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.Label(
                "Node Tree Editor v1.0",
                new GUIStyle(EditorStyles.centeredGreyMiniLabel) { fontSize = 9 }
            );

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
    }
}