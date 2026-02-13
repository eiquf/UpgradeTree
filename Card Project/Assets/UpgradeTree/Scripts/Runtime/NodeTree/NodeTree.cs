//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Root container of the Upgrade Tree system.
/// 
/// NodeTree is a standalone ScriptableObject and must exist as a single,
/// authoritative instance per upgrade tree.
/// 
/// Responsibilities:
/// - Owns and manages all Node instances belonging to the tree
/// - Stores and validates the global ID registry used by nodes
/// - Acts as the single entry point for editor tools and runtime systems
/// 
/// All child Nodes are conceptually owned by this NodeTree and should be:
/// - Created
/// - Removed
/// - Linked
/// - Modified
/// exclusively through this asset or systems operating on it.
///
/// Nodes should not exist or be manipulated independently of a NodeTree.
/// Any editor or runtime logic must treat NodeTree as the source of truth.
/// </summary>
namespace Eiquif.UpgradeTree.Runtime
{
    [CreateAssetMenu(fileName = "NodeTree", menuName = "UpgradeTree/NodeTree")]
    public class NodeTree : ScriptableObject
    {
        /// <summary>
        /// All nodes belonging to this tree.
        /// Nodes are considered children of this NodeTree and should not be shared
        /// between different trees.
        /// </summary>
        public List<Node> Nodes = new();
        /// <summary>
        /// Registry of all valid IDs available for nodes in this tree.
        /// Acts as a centralized ID database to ensure consistency and validation.
        /// </summary>
        public List<string> IDs = new();

        public ProgressionProviderSO ProgressionProvider;
        public UnlockConditionSO UnlockCondition;
    }
}
