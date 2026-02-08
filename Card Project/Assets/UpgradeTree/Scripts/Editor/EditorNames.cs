//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public abstract class EditorNames
    {
        protected readonly ContextSystem Context;

        protected EditorNames(ContextSystem context)
        {
            Context = context;
        }
        public void DrawHeader()
        {
            var rect = EditorGUILayout.GetControlRect(false, HeaderHeight);
            OnDrawHeader(rect);
        }
        public void DrawFooter()
        {
            OnDrawFooter();
        }
        protected virtual float HeaderHeight => 50f;
        protected abstract void OnDrawHeader(Rect rect);
        protected abstract void OnDrawFooter();
    }
}