using System;
using UnityEngine;

[Serializable]
/// <summary>
/// Represents a unique identifier for a node in the node tree.
/// The <see cref="NodeID"/> class holds a string value that serves as the identifier for a node.
/// </summary>
public class NodeID
{
    /// <summary>
    /// The value of the node ID. This is a string that uniquely identifies a node within the tree.
    /// </summary>
    public string Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="NodeID"/> class.
    /// </summary>
    public NodeID() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="NodeID"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value to set for the node ID.</param>
    public NodeID(string value) => Value = value;
}
