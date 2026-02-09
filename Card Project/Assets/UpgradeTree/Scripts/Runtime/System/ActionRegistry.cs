//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System;
using System.Collections.Generic;

namespace Eiquif.UpgradeTree.Runtime
{
    public class ActionRegistry
    {
        private readonly Dictionary<string, Action<SkillSO>> _events = new();

        public void Subscribe(string id, Action<SkillSO> callback)
        {
            if (string.IsNullOrEmpty(id)) return;
            if (!_events.ContainsKey(id)) _events[id] = delegate { };
            _events[id] += callback;
        }

        public void Unsubscribe(string id, Action<SkillSO> callback)
        {
            if (_events.ContainsKey(id)) _events[id] -= callback;
        }

        public void Invoke(Node node)
        {
            if (_events.TryGetValue(node.ID.Value, out var action))
            {
                action?.Invoke(node.Stats);
            }
        }
    }
}