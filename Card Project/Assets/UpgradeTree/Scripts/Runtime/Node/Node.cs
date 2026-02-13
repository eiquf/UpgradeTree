//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************

/// <summary>
/// Represents a single upgrade node within a NodeTree.
///
/// A Node is not a standalone asset and must always be owned by exactly one
/// NodeTree instance. It exists only in the context of its parent tree and
/// should never be created, duplicated, or modified independently.
///
/// Responsibilities:
/// - Stores node-specific metadata (ID, name, description)
/// - Defines graph connections to other nodes (next and prerequisite nodes)
/// - Contains unlock requirements and progression rules
/// - Holds visual and gameplay-related data used at runtime and in the editor
///
/// Ownership & Rules:
/// - Nodes are conceptually children of a NodeTree
/// - A Node must not be shared between multiple NodeTrees
/// - All structural changes (creation, removal, linking, ID assignment)
///   must be performed through the owning NodeTree or its editor systems
///
/// Any editor or runtime system should treat the NodeTree as the source of truth
/// and assume that Nodes are valid only while referenced by that tree.
/// </summary>
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
        [SerializeField] private int _currentLevel;
        public bool _unlockIfParentMax = true;

        [Header("Other")]
        [SerializeField] private SkillSO _stats;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Sprite _frame;
        [SerializeField] private Color _unlockedColor;
        [SerializeField] private Color _unlockableColor;
        public Vector2 position;

        [SerializeField]
        private List<UnlockConditionSO> _conditions = new();

        public IReadOnlyList<UnlockConditionSO> Conditions => _conditions;

        public NodeID ID => _id;
        public string Name => _displayName;
        public Sprite Icon => _icon;
        public Sprite Frame => _frame;
        public SkillSO Stats => _stats;
        public int ParentUnlockLevel => _parentLevelUnlock;
        public int Cost => _cost;
        public bool IsRoot => PrerequisiteNodes == null || PrerequisiteNodes.Count == 0;
        public Color UnlockedColor => _unlockedColor;
        public Color UnlockableColor => _unlockableColor;
    }
}