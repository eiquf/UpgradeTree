using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Eiquif.UpgradeTree.Editor
{
    public class UpgradeGraphView : GraphView
    {
        public Action<UpgradeNodeView> OnSelect;
        private readonly UpgradeTreeEditor _editor;

        public UpgradeGraphView(UpgradeTreeEditor editor)
        {
            _editor = editor;

            Insert(0, new GridBackground());

            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            graphViewChanged = OnGraphViewChanged;

            RegisterCallback<MouseDownEvent>(_ =>
            {
                OnSelect?.Invoke(selection.OfType<UpgradeNodeView>().FirstOrDefault());
            });
        }
        private GraphViewChange OnGraphViewChanged(GraphViewChange change)
        {
            if (change.edgesToCreate != null)
                foreach (var e in change.edgesToCreate)
                    _editor?.OnEdgeCreated(e);

            if (change.elementsToRemove != null)
            {
                foreach (var e in change.elementsToRemove)
                {
                    if (e is UpgradeNodeView node)
                        _editor?.OnNodeRemoved(node);

                    if (e is Edge edge)
                        _editor?.OnEdgeRemoved(edge);
                }
            }

            return change;
        }
        public void ClearGraph()
        {
            graphElements
                .Where(e => e is UpgradeNodeView || e is Edge)
                .ToList()
                .ForEach(RemoveElement);
        }
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter _)
        {
            return ports.Where(p =>
                p != startPort &&
                p.node != startPort.node &&
                p.direction != startPort.direction
            ).ToList();
        }
    }
}