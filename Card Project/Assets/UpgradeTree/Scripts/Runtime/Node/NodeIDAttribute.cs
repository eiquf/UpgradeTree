//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using System;
using UnityEngine;

namespace Eiquif.UpgradeTree.Runtime
{
    /// <summary>
    /// Attribute used to mark a string field as a Node ID reference.
    /// </summary>
    /// <remarks>
    /// When applied, a custom property drawer displays a dropdown
    /// populated with IDs from the referenced <see cref="NodeTree"/>.
    /// </remarks>
    /// <example>
    /// <code>
    /// [SerializeField] private NodeTree _tree;
    ///
    /// [NodeID(nameof(_tree))]
    /// [SerializeField] private string _nodeID;
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class NodeIDAttribute : PropertyAttribute
    {
        /// <summary>
        /// Name of the field that contains the <see cref="NodeTree"/>
        /// used to provide available Node IDs.
        /// </summary>
        public string TreeFieldName;

        /// <param name="treeFieldName">
        /// Name of the field referencing a <see cref="NodeTree"/>.
        /// </param>
        public NodeIDAttribute(string treeFieldName)
        {
            TreeFieldName = treeFieldName;
        }
    }
}