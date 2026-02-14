//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    [CustomEditor(typeof(ProgressionProviderSO), true)]
    public class ProgressionEditor : UnityEditor.Editor
    {
        private ProgressionProviderSO _so;
        private ProgressionContext _ctx;

        private EditorNames _names;
        private Section _resetSection;
        private Section _otherSection;
        private void OnEnable()
        {
            _so = (ProgressionProviderSO)target;

            _ctx = new ProgressionContext(_so, serializedObject);

            _names = new ProgressionEditorNames(_ctx, _so.name);
            _resetSection = new ResetSection(_so);
            _otherSection = new ProgressionOthersSection(_ctx);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _names.DrawHeader();

            GUILayout.Space(12);
            _resetSection.Draw();

            _otherSection.Draw();
            GUILayout.Space(12);
            _names.DrawFooter();

            serializedObject.ApplyModifiedProperties();
        }
    }
}