using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeNodeView : UnityEditor.Experimental.GraphView.Node
{
    public Node Data { get; }
    public Port In { get; }
    public Port Out { get; }

    public UpgradeNodeView(Node data)
    {
        Data = data;

        title = string.IsNullOrEmpty(data.Name) ? data.name : data.Name;
        viewDataKey = data.GetInstanceID().ToString();

        SetPosition(new Rect(data.position, new Vector2(220, 130)));

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

            titleContainer.Insert(0, icon);
        }

        In = InstantiatePort(
            Orientation.Horizontal,
            Direction.Input,
            Port.Capacity.Multi,
            typeof(bool)
        );
        In.portName = "In";
        inputContainer.Add(In);

        Out = InstantiatePort(
            Orientation.Horizontal,
            Direction.Output,
            Port.Capacity.Multi,
            typeof(bool)
        );
        Out.portName = "Out";
        outputContainer.Add(Out);

        RefreshExpandedState();
        RefreshPorts();
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);

        Data.position = newPos.position;
        EditorUtility.SetDirty(Data);
    }
}
