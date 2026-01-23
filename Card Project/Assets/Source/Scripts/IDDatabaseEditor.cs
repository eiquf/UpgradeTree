using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IDDatabase))]
public class IDDatabaseEditor : Editor
{
    //SerializedProperty idsProp;
    //string newID = "";

    //private void OnEnable() => idsProp = serializedObject.FindProperty("IDs");
    //public override void OnInspectorGUI()
    //{
    //    serializedObject.Update();

    //    EditorGUILayout.LabelField("ID Manager", EditorStyles.boldLabel);

    //    EditorGUILayout.BeginHorizontal();
    //    newID = EditorGUILayout.TextField("New ID", newID);

    //    GUI.enabled = !string.IsNullOrEmpty(newID);

    //    if (GUILayout.Button("Add", GUILayout.Width(60))) AddID(newID);

    //    GUI.enabled = true;

    //    EditorGUILayout.EndHorizontal();
    //    EditorGUILayout.Space(5);

    //    for (int i = 0; i < idsProp.arraySize; i++)
    //    {
    //        EditorGUILayout.BeginHorizontal();
    //        EditorGUILayout.LabelField(idsProp.GetArrayElementAtIndex(i).stringValue);

    //        if (GUILayout.Button("X", GUILayout.Width(20)))
    //        {
    //            idsProp.DeleteArrayElementAtIndex(i);
    //            break;
    //        }

    //        EditorGUILayout.EndHorizontal();
    //    }
    //    serializedObject.ApplyModifiedProperties();
    //}

    //private void AddID(string id)
    //{
    //    if (((IDDatabase)target).IDs.Contains(id)) return;

    //    idsProp.InsertArrayElementAtIndex(idsProp.arraySize);
    //    idsProp.GetArrayElementAtIndex(idsProp.arraySize - 1).stringValue = id;
    //    newID = "";
    //}
}