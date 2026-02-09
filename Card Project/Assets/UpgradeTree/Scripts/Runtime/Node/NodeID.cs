//***************************************************************************************
// Writer: Eiquif
// Last Updated: January 2026
//***************************************************************************************

/// <summary>
/// Represents a unique identifier for an upgrade tree node.
/// </summary>
/// <remarks>
/// Used to reference and compare nodes independently from their runtime instances.
/// Serializable to support Unity inspector and asset persistence.
/// </remarks>
using System;
[Serializable]
public class NodeID
{
    /// <summary>
    /// Raw string value of the node identifier.
    /// </summary>
    public string Value;

    /// <summary>
    /// Creates an empty node identifier.
    /// </summary>
    public NodeID() { }

    /// <summary>
    /// Creates a node identifier with the given value.
    /// </summary>
    /// <param name="value">Unique string value of the node.</param>
    public NodeID(string value) => Value = value;
}