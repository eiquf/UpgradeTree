using UnityEngine;

public abstract class NodeAction : ScriptableObject
{
    public abstract void Execute(Node node);
}
