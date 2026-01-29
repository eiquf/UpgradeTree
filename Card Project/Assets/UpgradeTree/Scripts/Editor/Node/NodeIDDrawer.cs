using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UpgradeTree.Nodes
{
    //[CustomPropertyDrawer(typeof(NodeID))]
    public class NodeIDDrawer /*: PropertyDrawer*/
    {
        //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        //{
        //    NodeTree tree = GetNodeTree();
        //    SerializedProperty valueProp = property.FindPropertyRelative("Value");

        //    if (tree == null || tree.IDs == null || tree.IDs.Count == 0)
        //    {
        //        DrawNoneOnly(position, label, valueProp);
        //        return;
        //    }

        //    string[] options = new[] { "None" }
        //        .Concat(tree.IDs)
        //        .ToArray();

        //    int currentIndex = 0;

        //    if (!string.IsNullOrEmpty(valueProp.stringValue))
        //    {
        //        int foundIndex = System.Array.IndexOf(options, valueProp.stringValue);
        //        currentIndex = Mathf.Max(0, foundIndex);
        //    }

        //    int newIndex = EditorGUI.Popup(position, label.text, currentIndex, options);

        //    if (newIndex == 0)
        //        valueProp.stringValue = null;
        //    else if (newIndex > 0 && newIndex < options.Length)
        //        valueProp.stringValue = options[newIndex];
        //}

        //private void DrawNoneOnly(Rect position, GUIContent label, SerializedProperty valueProp)
        //{
        //    string[] options = { "None" };
        //    EditorGUI.Popup(position, label.text, 0, options);
        //    valueProp.stringValue = null;
        //}

        //public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        //    => EditorGUIUtility.singleLineHeight;

        //private NodeTree GetNodeTree()
        //{
        //    string[] guids = AssetDatabase.FindAssets("t:NodeTree");
        //    if (guids.Length == 0) return null;

        //    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        //    return AssetDatabase.LoadAssetAtPath<NodeTree>(path);
        //}
    }
}