namespace Eiquif.UpgradeTree.Editor.TreeWindow
{
    using UnityEditor;
    using UnityEditor.Experimental.GraphView;
    using UnityEditor.UIElements;
    using UnityEngine;
    using UnityEngine.UIElements;
    using GraphNode = UnityEditor.Experimental.GraphView.Node;
    using RuntimeNode = Runtime.Node.Node;

    public class UpgradeNodeView : GraphNode
    {
        public RuntimeNode Data { get; }
        private Foldout _foldout;
        public Port In { get; }
        public Port Out { get; }

        public UpgradeNodeView(RuntimeNode data)
        {
            Data = data;

            title = string.IsNullOrEmpty(data.Name) ? data.name : data.Name;
            viewDataKey = data.GetInstanceID().ToString();

            SetPosition(new Rect(data.position, new Vector2(220, 130)));

            CreateFoldout();

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
        private void CreateFoldout()
        {
            _foldout = new Foldout()
            {
                text = "Node settings",
                value = false
            };

            var SO = new SerializedObject(Data);

            _foldout.Add(new InspectorElement(SO));

            extensionContainer.Add(_foldout);
        }
    }
}