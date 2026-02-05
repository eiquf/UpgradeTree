//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using GraphNode = UnityEditor.Experimental.GraphView.Node;
using RuntimeNode = Eiquif.UpgradeTree.Runtime.Node;

namespace Eiquif.UpgradeTree.Editor
{
    public class UpgradeNodeView : GraphNode
    {
        public RuntimeNode Data { get; }

        private readonly GraphNode _graph;
        public Port In { get; }
        public Port Out { get; }

        private IElement<RuntimeNode> _foldoutView;
        private IElement<RuntimeNode> _icon;
        public UpgradeNodeView(RuntimeNode data)
        {
            Data = data;
            _graph = this;

            title = string.IsNullOrEmpty(data.Name) ? data.name : data.Name;
            viewDataKey = data.GetInstanceID().ToString();

            SetPosition(new Rect(data.position, new Vector2(220, 130)));

            Init();
            CreateFoldout();
            Icon();

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
        private void Init()
        {
            _foldoutView = new CreateNodeViewFoldOut(_graph);
            _icon = new CreateIconNodeView(_graph);
        }
        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);

            Data.position = newPos.position;
            EditorUtility.SetDirty(Data);
        }
        private void Icon() => _icon.Execute(Data);
        private void CreateFoldout() => _foldoutView.Execute(Data);
    }
}