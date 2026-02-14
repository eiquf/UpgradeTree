//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public static class NodeIDPicker
    {
        public static string Draw(
            Rect rect,
            string currentValue,
            IReadOnlyList<string> ids
        )
        {
            if (ids == null || ids.Count == 0)
            {
                EditorGUI.LabelField(rect, "No IDs");
                return currentValue;
            }

            int currentIndex = Mathf.Max(0, ids.IndexOf(currentValue));

            int newIndex = EditorGUI.Popup(
                rect,
                currentIndex,
                ids as string[] ?? new List<string>(ids).ToArray()
            );

            return newIndex >= 0 && newIndex < ids.Count
                ? ids[newIndex]
                : currentValue;
        }
    }

    internal static class ListExtensions
    {
        public static int IndexOf<T>(this IReadOnlyList<T> list, T value)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(list[i], value))
                    return i;
            }
            return -1;
        }
    }
}