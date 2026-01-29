using System;
using System.Collections.Generic;

public class ActionExecutor
{
    public void Run(Action<Node> action, IEnumerable<Node> nodes)
    {
        foreach (var node in nodes)
            action?.Invoke(node);
    }

    public void Run(Action<Node> action, Node node) => action?.Invoke(node);
}