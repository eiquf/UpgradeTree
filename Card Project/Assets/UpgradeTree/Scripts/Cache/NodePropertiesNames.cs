//***************************************************************************************
// Writer: Eiquif
// Last Updated: January 2026
// Description: ScriptableObject representing a single upgrade node.
//              Stores metadata, graph connections, requirements,
//              and visual information used at runtime and in the editor.
//***************************************************************************************
public static class NodePropertiesNames
{
    public static string ID { get; } = "_id";
    public static string Description { get; } = "_description";
    public static string Name { get; } = "_displayName";
    public static string NextNodes { get; } = "NextNodes";
    public static string PrerequisiteNodes { get; } = "PrerequisiteNodes";
    public static string Cost { get; } = "_cost";
    public static string LevelUnlock { get; } = "_parentLevelUnlock";
    public static string UnlockIfParentMax { get; } = "_unlockIfParentMax";
    public static string Stats { get; } = "_stats";
    public static string Icon { get; } = "_icon";
    public static string Position { get; } = "position";
}