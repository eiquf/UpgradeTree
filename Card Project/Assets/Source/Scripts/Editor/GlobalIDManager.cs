using System.Collections.Generic;
using UnityEditor;

public static class GlobalIDManager
{
    private static HashSet<string> usedIDs = new();

    public static void LoadAllIDs()
    {
        usedIDs.Clear();

        string[] guids = AssetDatabase.FindAssets("t:NodeTree");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            //NodeTree tree = AssetDatabase.LoadAssetAtPath<NodeTree>(path);

            //if (tree != null && tree.IDs != null)
            //{
            //    foreach (string id in tree.IDs)
            //        usedIDs.Add(id);
            //}
        }
    }

    public static bool IsIDUnique(string id) => !usedIDs.Contains(id);

    public static void AddID(string id) => usedIDs.Add(id);
}
