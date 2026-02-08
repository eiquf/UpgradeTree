using UnityEditor;
using UnityEngine;

public static class EditorStyleCache
{
    public static GUIStyle HeaderStyle { get; private set; }
    public static GUIStyle BoxStyle { get; private set; }
    public static GUIStyle CardStyle { get; private set; }
    public static GUIStyle MiniButtonStyle { get; private set; }
    public static GUIStyle TagStyle { get; private set; }
    public static GUIStyle CenteredLabelStyle { get; private set; }
    public static GUIStyle StatusBadgeStyle { get; private set; }

    private static bool _stylesInitialized;
    
    static EditorStyleCache()
    {
        if(_stylesInitialized == false) InitializeStyles();
    }
    private static void InitializeStyles()
    {
        if (_stylesInitialized) return;

        HeaderStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 14,
            alignment = TextAnchor.MiddleLeft,
            padding = new RectOffset(8, 8, 4, 4)
        };
        HeaderStyle.normal.textColor = Color.white;

        BoxStyle = new GUIStyle("box")
        {
            padding = new RectOffset(12, 12, 8, 8),
            margin = new RectOffset(0, 0, 4, 4)
        };

        CardStyle = new GUIStyle(EditorStyles.helpBox)
        {
            padding = new RectOffset(10, 10, 8, 8),
            margin = new RectOffset(4, 4, 4, 4)
        };

        MiniButtonStyle = new GUIStyle(EditorStyles.miniButton)
        {
            fontSize = 10,
            padding = new RectOffset(6, 6, 2, 2)
        };

        TagStyle = new GUIStyle(EditorStyles.miniLabel)
        {
            fontSize = 9,
            alignment = TextAnchor.MiddleCenter,
            padding = new RectOffset(6, 6, 2, 2)
        };
        TagStyle.normal.textColor = Color.white;

        CenteredLabelStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
        {
            fontSize = 10
        };

        StatusBadgeStyle = new GUIStyle(EditorStyles.miniLabel)
        {
            fontSize = 10,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        };

        _stylesInitialized = true;
    }
}