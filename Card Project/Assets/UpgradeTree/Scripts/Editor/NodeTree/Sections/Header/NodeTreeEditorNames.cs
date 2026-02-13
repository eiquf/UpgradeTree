//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public class NodeTreeEditorNames : EditorNames
    {
        private readonly NodeTreeContext _ctx;
        private readonly NodeTreeHeaderContext _headerCtx;

        private readonly IElement<NodeTreeHeaderContext>[] _headerElements;
        private readonly IElement<ContextSystem>[] _footerElements;
        private readonly IElement[] _staticElements;

        public NodeTreeEditorNames(ContextSystem context, string name)
            : base(context)
        {
            _ctx = (NodeTreeContext)context;

            _headerCtx = new NodeTreeHeaderContext
            {
                Name = name,
                TreeContext = _ctx
            };

            _headerElements = new IElement<NodeTreeHeaderContext>[]
            {
                new HeaderBackgroundElement(),
                new HeaderBordersElement(),
                new HeaderIconsElement(),
                new HeaderTitleElement(),
                new HeaderStatusBadgeElement()
            };

            _footerElements = new IElement<ContextSystem>[]
            {
                new FooterButtonsElement()
            };

            _staticElements = new IElement[]
            {
                new FooterTextElement()
            };
        }
        protected override void OnDrawFooter()
        {
            foreach (var element in _footerElements)
                element.Execute(_ctx);

            foreach (var element in _staticElements)
                element.Execute();
        }

        protected override void OnDrawHeader(Rect rect)
        {
            _headerCtx.Rect = rect;

            foreach (var element in _headerElements)
                element.Execute(_headerCtx);
        }
    }
}