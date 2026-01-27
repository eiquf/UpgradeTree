using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NodeTree", menuName = "NodeTree/NodeTree")]
public class NodeTree : ScriptableObject
{
    public List<Node> Nodes = new();
    public List<string> IDs = new();
    public List<Action<Node>> Actions = new();

    public void AddAction(Action<Node> newAction) => Actions.Add(newAction);
    public void RemoveAction(Action<Node> actionToRemove) => Actions.Remove(actionToRemove);
}

