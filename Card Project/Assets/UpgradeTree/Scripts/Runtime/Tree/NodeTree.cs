namespace Eiquif.UpgradeTree.Runtime.Tree
{
    using Runtime.Node;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using System.Linq;

    [CreateAssetMenu(fileName = "NodeTree", menuName = "NodeTree/NodeTree")]
    public class NodeTree : ScriptableObject
    {
        public List<Node> Nodes = new();
        public List<string> IDs = new();

#if UNITY_EDITOR
        public void RemoveAllNodes()
        {
            Undo.RecordObject(this, "Remove All Nodes");

            var nodesCopy = Nodes.ToList();

            foreach (var node in nodesCopy)
            {
                if (node == null) continue;

                Undo.DestroyObjectImmediate(node);
            }

            Nodes.Clear();

            EditorUtility.SetDirty(this);
        }
#endif
#if UNITY_EDITOR
        public void RemoveNodeSafe(Node node)
        {
            if (node == null) return;

            Undo.RecordObject(this, "Remove Node");

            Nodes.Remove(node);

            foreach (var n in Nodes)
            {
                if (n == null) continue;

                n.NextNodes.Remove(node);
                n.PrerequisiteNodes.Remove(node);
                EditorUtility.SetDirty(n);
            }

            Undo.DestroyObjectImmediate(node);

            EditorUtility.SetDirty(this);
        }
#endif
#if UNITY_EDITOR
        public Node CreateNode()
        {
            Undo.RecordObject(this, "Create Node");

            var node = ScriptableObject.CreateInstance<Node>();
            node.name = $"Node_{Nodes.Count}";

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this));

            Undo.RegisterCreatedObjectUndo(node, "Create Node");

            Nodes.Add(node);

            EditorUtility.SetDirty(node);
            EditorUtility.SetDirty(this);

            return node;
        }
#endif
    }
}