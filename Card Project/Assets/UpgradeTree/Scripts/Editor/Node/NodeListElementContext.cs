//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEngine;

public class NodeListElementContext
{
    public Rect Rect;
    public SerializedProperty Property;
    public Node Node;
    public int Index;
    public ContextSystem Ctx;

    public bool HasId => Node != null && !string.IsNullOrEmpty(Node.ID.Value);
}