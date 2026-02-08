//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public static class EditorDrawPrimitives
    {
        public static void DrawGradientRect(
            Rect rect,
            Color top,
            Color bottom,
            int steps = 10)
        {
            var stepHeight = rect.height / steps;

            for (var i = 0; i < steps; i++)
            {
                var t = (float)i / steps;
                var color = Color.Lerp(top, bottom, t);
                var stepRect = new Rect(
                    rect.x,
                    rect.y + i * stepHeight,
                    rect.width,
                    stepHeight + 1);

                EditorGUI.DrawRect(stepRect, color);
            }
        }

        public static void DrawBorder(
            Rect rect,
            Color color,
            float thickness = 1f)
        {
            EditorGUI.DrawRect(new Rect(rect.x, rect.y, rect.width, thickness), color);
            EditorGUI.DrawRect(new Rect(rect.x, rect.yMax - thickness, rect.width, thickness), color);
            EditorGUI.DrawRect(new Rect(rect.x, rect.y, thickness, rect.height), color);
            EditorGUI.DrawRect(new Rect(rect.xMax - thickness, rect.y, thickness, rect.height), color);
        }
    }
}