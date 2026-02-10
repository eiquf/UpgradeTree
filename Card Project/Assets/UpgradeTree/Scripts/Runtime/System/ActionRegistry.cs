//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Eiquif.UpgradeTree.Runtime
{
    public static class ActionRegistry
    {
        private static readonly Dictionary<string, Action<SkillSO>> _events = new();

        public static void Subscribe(string id, Action<SkillSO> callback)
        {
            if (string.IsNullOrEmpty(id)) return;

            if (!_events.ContainsKey(id))
                _events[id] = delegate { };

            _events[id] += callback;
        }

        public static void Unsubscribe(string id, Action<SkillSO> callback)
        {
            if (_events.ContainsKey(id))
                _events[id] -= callback;
        }

        public static void Invoke(Node node)
        {
            if (node == null) return;

            if (_events.TryGetValue(node.ID.Value, out var action))
                action?.Invoke(node.Stats);
        }

        public static void Clear()
        {
            _events.Clear();
        }
    }
}