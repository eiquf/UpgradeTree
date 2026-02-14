//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEngine;
using UnityEngine.UIElements;
using GraphNode = UnityEditor.Experimental.GraphView.Node;

namespace Eiquif.UpgradeTree.Editor
{
    public class CreateIconNodeView : IElement<Node>
    {
        private readonly GraphNode _graph;
        public CreateIconNodeView(GraphNode graph) => _graph = graph;
        public void Execute(Node data)
        {
            if (data.Icon != null)
            {
                var icon = new Image
                {
                    image = data.Icon.texture,
                    scaleMode = ScaleMode.ScaleToFit
                };

                icon.style.width = 32;
                icon.style.height = 32;
                icon.style.marginRight = 4;

                _graph.titleContainer.Insert(0, icon);
            }
        }
    }
}