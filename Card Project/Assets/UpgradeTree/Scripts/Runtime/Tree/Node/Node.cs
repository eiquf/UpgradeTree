using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNode", menuName = "NodeTree/Node")]
public class Node : ScriptableObject
{
    [field: Header("Info")]
    public NodeID ID;
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public string Name { get; private set; }

    [field: Header("Graph")]
    [field: SerializeField] public List<Node> nextNodes { get; private set; } = new();
    [field: SerializeField] public List<Node> prerequisiteNodes { get; private set; } = new();


    [field: Header("Requirments")]
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public int MaxLevel { get; private set; }
    [field: Header("Other")]
    [field: SerializeField] public SkillSO stats { get; private set; }
    [field: SerializeField] public Vector2 position { get; private set; }
    [field: SerializeField] public Sprite icon { get; private set; }
}
