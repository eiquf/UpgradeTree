//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using System.Collections.Generic;

namespace Eiquif.UpgradeTree.Editor
{
    public class NodeGraphSection : Section
    {
        private readonly NodeContext _ctx;

        private readonly List<IElement<NodeContext>> _elements;

        public NodeGraphSection(ContextSystem ctx) : base(ctx)
        {
            _ctx = (NodeContext)ctx;

            _elements = new()
        {
            new NextNodesSectionElement(),
            new PrerequisiteNodesSectionElement(),
            new NodeGraphAnimationElement()
        };
        }

        public override void Draw()
        {
            foreach (var e in _elements)
                e.Execute(_ctx);
        }
    }
}