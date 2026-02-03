using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeGraphView : GraphView
{
    public Action<UpgradeNodeView> OnSelect;
    public UpgradeTreeEditor Editor;

    public UpgradeGraphView()
    {
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

    GraphViewChange OnGraphViewChanged(GraphViewChange change)
    {
        if (change.edgesToCreate != null)
            foreach (var e in change.edgesToCreate)
                Editor?.OnEdgeCreated(e);

        if (change.elementsToRemove != null)
        {
            foreach (var e in change.elementsToRemove)
            {
                if (e is UpgradeNodeView node)
                    Editor?.OnNodeRemoved(node);

                if (e is Edge edge)
                    Editor?.OnEdgeRemoved(edge);
            }
        }

        return change;
    }

    public void ClearGraph()
    {
        graphElements
            .Where(e => e is UpgradeNodeView || e is Edge)
            .ToList()
            .ForEach(RemoveElement); // ❗ НЕ DeleteElements
    }


    public override System.Collections.Generic.List<Port>
        GetCompatiblePorts(Port startPort, NodeAdapter _)
    {
        return ports.Where(p =>
            p != startPort &&
            p.node != startPort.node &&
            p.direction != startPort.direction
        ).ToList();
    }
}
