using UnityEngine;

[CreateAssetMenu(menuName = "NodeTree/Actions/Log")]
public class LogNodeAction : NodeAction
{
    public enum F { };
    public string messagePrefix = "Node Log";
    public override void Execute(Node node) => Debug.Log($"{messagePrefix}: {node.name}");
}