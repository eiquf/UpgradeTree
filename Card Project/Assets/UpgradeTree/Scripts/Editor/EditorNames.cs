namespace Eiquif.UpgradeTree.Editor
{
    using Eiquif.UpgradeTree.Runtime;
    using UnityEditor;
    using UnityEngine;

    public abstract class EditorNames
    {
        protected ContextSystem context;
        protected Rect headerRect;

        public EditorNames(ContextSystem context) => this.context = context;

        public void DrawHeader()
        {
            headerRect = EditorGUILayout.GetControlRect(false, 50);

            DrawBackground(headerRect);
            DrawBorders(headerRect);
            DrawIcons(headerRect);
            DrawTitle(headerRect);
            DrawStatusBadge(headerRect);
        }

        protected virtual void DrawBackground(Rect rect) { }
        protected virtual void DrawBorders(Rect rect) { }
        protected virtual void DrawIcons(Rect rect) { }
        protected virtual void DrawTitle(Rect rect) { }
        protected virtual void DrawStatusBadge(Rect rect) { }

        public virtual void DrawFooter()
        {
            DrawFooterButtons();
            DrawFooterText();
        }

        protected abstract void DrawFooterButtons();
        protected abstract void DrawFooterText();
    }
}