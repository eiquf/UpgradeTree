using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Node : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private NodeID id;
    [SerializeField, TextArea(3, 6)] private string description;
    [SerializeField] private string displayName;

    [Header("Graph")]
    public List<Node> NextNodes = new();
    public List<Node> PrerequisiteNodes = new();

    [Header("Requirements")]
    [SerializeField] private int cost;
    [SerializeField] private int maxLevel;
    [SerializeField] public bool isLocked = true;

    [Header("Other")]
    [SerializeField] private SkillSO stats;
    [SerializeField] private Sprite icon;
    public Vector2 position;

    public NodeID ID => id;
    public string Description => description;
    public string Name => displayName;
    public Sprite Icon => icon;
    public int Cost => cost;
    public int MaxLevel => maxLevel;
}
