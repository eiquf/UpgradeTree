//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Editor;
using UnityEngine;

public sealed class EditorDrawBorders : IEditorElement
{
    private readonly float _thickness;
    private readonly Color _color;

    public EditorDrawBorders(Color color, float thickness = 1f)
    {
        _color = color;
        _thickness = thickness;
    }

    public void Draw(Rect rect)
    {
        EditorDrawPrimitives.DrawBorder(
            rect, _color, _thickness);
    }
}