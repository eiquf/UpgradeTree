//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
// Description: ScriptableObject representing a single upgrade node.
//              Stores metadata, graph connections, requirements,
//              and visual information used at runtime and in the editor.
//***************************************************************************************
using System.Collections.Generic;
using UnityEngine;

namespace Eiquif.UpgradeTree.Runtime
{
    public class Node : ScriptableObject
    {
        [Header("Info")]
        [SerializeField] private NodeID _id;
        [SerializeField] private string _displayName;
        [SerializeField, TextArea(3, 6)] private string _description;

        [Header("Graph")]
        public List<Node> NextNodes = new();
        public List<Node> PrerequisiteNodes = new();

        [Header("Requirements")]
        [SerializeField] private int _cost;
        [SerializeField] private int _parentLevelUnlock;
        public bool _unlockIfParentMax = true;

        [Header("Other")]
        [SerializeField] private SkillSO _stats;
        [SerializeField] private Sprite _icon;
        public Vector2 position;

        public NodeID ID => _id;
        public string Name => _displayName;
        public Sprite Icon => _icon;
    }
}