using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class NodeValidationDrawer
{
    private readonly EditorFlowerAnimation _anim = new();

    public void Draw(List<Node> nodes, Object undoTarget)
    {
        if (nodes == null || nodes.Count == 0) return;

        // 1. Пустые слоты (null в списке)
        var nullCount = nodes.Count(n => n == null);

        // 2. Узлы с отсутствующим ID (только для существующих объектов)
        var noIdCount = nodes.Count(n => n != null && string.IsNullOrEmpty(n.ID.Value));

        // 3. Реальные дубликаты (одинаковый ID у разных или тех же объектов)
        var duplicateGroups = nodes
            .Where(n => n != null && !string.IsNullOrEmpty(n.ID.Value))
            .GroupBy(n => n.ID.Value)
            .Where(g => g.Count() > 1)
            .ToList();

        var duplicateCount = duplicateGroups.Sum(g => g.Count() - 1);

        if (nullCount == 0 && noIdCount == 0 && duplicateCount == 0) return;

        GUILayout.Space(8);
        EditorGUILayout.BeginVertical(EditorStyleCache.CardStyle);
        DrawHeader();

        if (nullCount > 0)
            DrawItem($"{nullCount} empty slot(s)", EditorColors.ErrorColor);

        if (noIdCount > 0)
            DrawItem($"{noIdCount} node(s) without ID", EditorColors.WarningColor);

        if (duplicateCount > 0)
            DrawItem($"{duplicateCount} duplicate ID(s) found", EditorColors.WarningColor);

        GUILayout.Space(8);

        // Кнопка очистки пустых ссылок
        if (nullCount > 0)
        {
            if (GUILayout.Button("🧹 Clean Empty Slots", GUILayout.Height(24)))
            {
                Undo.RecordObject(undoTarget, "Clean Null Nodes");
                int removed = nodes.RemoveAll(n => n == null);
                ApplyChanges(undoTarget, removed);
            }
        }

        // Кнопка удаления дубликатов (теперь безопасная)
        if (duplicateCount > 0)
        {
            if (GUILayout.Button("👯 Remove Duplicates", GUILayout.Height(24)))
            {
                Undo.RecordObject(undoTarget, "Remove Duplicate Nodes");

                var seenIds = new HashSet<string>();
                var uniqueList = new List<Node>();
                int removed = 0;

                foreach (var node in nodes)
                {
                    if (node == null)
                    {
                        uniqueList.Add(null); // Сохраняем null, если пользователь не нажал Clean
                        continue;
                    }

                    string id = node.ID.Value;
                    if (string.IsNullOrEmpty(id))
                    {
                        uniqueList.Add(node); // Узлы без ID не считаем дубликатами здесь
                        continue;
                    }

                    if (!seenIds.Contains(id))
                    {
                        seenIds.Add(id);
                        uniqueList.Add(node);
                    }
                    else
                    {
                        removed++;
                    }
                }

                nodes.Clear();
                nodes.AddRange(uniqueList);
                ApplyChanges(undoTarget, removed);
            }
        }

        EditorGUILayout.EndVertical();
    }

    private void ApplyChanges(Object undoTarget, int changeCount)
    {
        EditorUtility.SetDirty(undoTarget);
        if (changeCount > 0)
        {
            var rect = GUILayoutUtility.GetLastRect();
            _anim?.Spawn(rect.center, changeCount * 3);
        }
    }

    protected virtual void DrawHeader()
    {
        var rect = EditorGUILayout.GetControlRect(false, 24);
        EditorGUI.DrawRect(rect, EditorColors.WarningBgLight);
        var style = new GUIStyle(EditorStyles.boldLabel) { fontSize = 11, alignment = TextAnchor.MiddleLeft };
        style.normal.textColor = EditorColors.WarningColor;
        GUI.Label(new Rect(rect.x + 4, rect.y, rect.width, rect.height), "⚠️ Validation Issues", style);
    }

    protected virtual void DrawItem(string text, Color color)
    {
        EditorGUILayout.BeginHorizontal();
        var dot = EditorGUILayout.GetControlRect(GUILayout.Width(8), GUILayout.Height(16));
        EditorGUI.DrawRect(new Rect(dot.x, dot.y + 4, 8, 8), color);
        EditorGUILayout.LabelField(text);
        EditorGUILayout.EndHorizontal();
    }
}