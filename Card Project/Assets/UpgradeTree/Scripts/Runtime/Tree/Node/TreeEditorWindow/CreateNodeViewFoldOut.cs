using Eiquif.UpgradeTree.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using GraphNode = UnityEditor.Experimental.GraphView.Node;

namespace Eiquif.UpgradeTree.Editor
{
    public class CreateNodeViewFoldOut : IElement<Node>
    {
        private readonly GraphNode _graph;
        private Foldout _foldout;
        public CreateNodeViewFoldOut(GraphNode graph) => _graph = graph;
        public void Execute(Node data)
        {
            _foldout = new Foldout()
            {
                name = "Node Settings",
                value = false
            };

            var SO = new SerializedObject(data);

            _foldout.Add(new InspectorElement(SO));

            _graph.extensionContainer.Add(_foldout);
        }
    }
}