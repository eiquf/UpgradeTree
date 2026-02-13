//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public class ResetSection : Section
    {
        private readonly ProgressionContext _context;
        public ResetSection(ContextSystem context) : base(context) { _context = (ProgressionContext)context; }

        public override void Draw()
        {
            DrawButton();
        }

        private void DrawButton()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Debug Tools", EditorStyles.boldLabel);

            GUI.backgroundColor = new Color(1f, 0.4f, 0.4f);

            if (GUILayout.Button("Reset All Progression", GUILayout.Height(30)))
            {
                GUI.FocusControl(null);

                if (EditorUtility.DisplayDialog("Reset Progression?",
                    "Are you sure you want to wipe all progression data? This cannot be undone.",
                    "Yes, Reset", "Cancel"))
                {
                    _context.So.ResetProgression();

                    EditorUtility.SetDirty(_context.So);
                    AssetDatabase.SaveAssets();

                    Debug.Log("<color=red>Progression has been reset and saved.</color>");
                }
            }

            GUI.backgroundColor = Color.white;
            EditorGUILayout.EndVertical();
        }
    }
}