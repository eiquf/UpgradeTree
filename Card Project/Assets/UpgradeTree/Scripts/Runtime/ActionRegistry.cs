using System;
using System.Collections.Generic;

namespace Eiquif.UpgradeTree.Runtime
{
    public class ActionRegistry
    {
        private readonly Dictionary<string, Action<Node>> _events = new();

        public void Subscribe(string id, Action<Node> callback)
        {
            if (string.IsNullOrEmpty(id)) return;
            if (!_events.ContainsKey(id)) _events[id] = delegate { };
            _events[id] += callback;
        }

        public void Unsubscribe(string id, Action<Node> callback)
        {
            if (_events.ContainsKey(id)) _events[id] -= callback;
        }

        public void Invoke(string id, Node node)
        {
            if (_events.TryGetValue(id, out var action))
            {
                action?.Invoke(node);
            }
        }
    }
}