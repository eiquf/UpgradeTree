//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class NodeEditorNames : EditorNames
    {
        private readonly NodeContext _ctx;
        private readonly NodeHeaderContext _headerCtx;

        private readonly IElement<NodeHeaderContext>[] _header;
        private readonly IElement<ContextSystem>[] _footer;
        private readonly IElement[] _staticFooter;

        public NodeEditorNames(ContextSystem context, string name)
            : base(context)
        {
            _ctx = (NodeContext)context;

            _headerCtx = new NodeHeaderContext
            {
                Name = name,
                NodeContext = _ctx
            };

            _header = new IElement<NodeHeaderContext>[]
            {
                new NodeHeaderBackgroundElement(),
                new NodeHeaderBordersElement(),
                new NodeHeaderIconsElement(),
                new NodeHeaderTitleElement(),
                new NodeHeaderStatusBadgeElement()
            };

            _footer = new IElement<ContextSystem>[]
            {
                new FooterButtonsElement()
            };

            _staticFooter = new IElement[]
            {
                new FooterTextElement()
            };
        }

        protected override void OnDrawHeader(Rect rect)
        {
            _headerCtx.Rect = rect;

            foreach (var element in _header)
                element.Execute(_headerCtx);
        }

        protected override void OnDrawFooter()
        {
            foreach (var element in _footer)
                element.Execute(_ctx);

            foreach (var element in _staticFooter)
                element.Execute();
        }
    }
}