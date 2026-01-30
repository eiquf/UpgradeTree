using UnityEditor;

public class NodeContext : ContextSystem
{
    public SerializedObject SerializedObject { get; }
    public Node Node { get; }
    public NodeTree Tree { get; }
    public SerializedProperty NextProp { get; }
    public SerializedProperty PrerequisiteProp { get; }

    public double LastUpdateTime { get; private set; }

    public NodeContext(
        SerializedObject so,
        Node node,
        SerializedProperty next,
        SerializedProperty prerequisite)
    {

        IDMenu = new NodeIDMenu();

        SerializedObject = so;
        Node = node;
        NextProp = next;
        PrerequisiteProp = prerequisite;
    }

    public void UpdateTime(double time) => LastUpdateTime = time;
}
