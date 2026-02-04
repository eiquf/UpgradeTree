namespace Eiquif.UpgradeTree.Runtime.Node
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Node : ScriptableObject
    {
        [Header("Info")]
        [SerializeField] private NodeID _id;
        [SerializeField, TextArea(3, 6)] private string _description;
        [SerializeField] private string _displayName;

        [Header("Graph")]
        public List<Node> NextNodes = new();
        public List<Node> PrerequisiteNodes = new();

        [Header("Requirements")]
        [SerializeField] private Node _parent;
        [SerializeField] private int _cost;
        [SerializeField] private int _parentLevelUnlock;
        [SerializeField] public bool _unlockIfParentMax = true;

        [Header("Other")]
        [SerializeField] private SkillSO _stats;
        [SerializeField] private Sprite _icon;
        public Vector2 position;

        public NodeID ID => _id;
        public string Name => _displayName;
        public Sprite Icon => _icon;
    }
}