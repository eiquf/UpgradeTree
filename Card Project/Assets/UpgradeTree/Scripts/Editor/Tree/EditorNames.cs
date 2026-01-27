using UnityEditor;
using UnityEngine;

public abstract class EditorNames
{
    protected NodeTreeContext context;
    protected Rect headerRect;

    protected double lastUpdateTime;
    public EditorNames(NodeTreeContext context, double lastUpdateTime)
    {
        this.context = context;
        this.lastUpdateTime = lastUpdateTime;
    }

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
