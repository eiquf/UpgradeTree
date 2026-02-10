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
using UnityEngine;
using UnityEngine.UI;

namespace Eiquif.UpgradeTree.Runtime
{
    public static class NodeUIButtonState
    {
        public static void Apply(NodeUIState state, GameObject go)
        {
            var button = go.GetComponent<Button>();
            var cg = go.GetComponent<CanvasGroup>() ?? go.AddComponent<CanvasGroup>();

            switch (state)
            {
                case NodeUIState.Hidden:
                    cg.alpha = 0f;
                    button.interactable = false;
                    break;

                case NodeUIState.Locked:
                    cg.alpha = 0.4f;
                    button.interactable = false;
                    break;

                case NodeUIState.Unlockable:
                    cg.alpha = 1f;
                    button.interactable = true;
                    break;

                case NodeUIState.Unlocked:
                    cg.alpha = 1f;
                    button.interactable = false;
                    break;
            }
        }
    }
}
