//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using UnityEngine;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class ProgressionEditorNames : EditorNames
    {
        private readonly ProgressionContext _ctx;
        private readonly ProgressionHeaderContext _headerCtx;

        private readonly IElement<ProgressionHeaderContext>[] _header;
        private readonly IElement<ContextSystem>[] _footer;
        private readonly IElement[] _staticFooter;

        public ProgressionEditorNames(ContextSystem context, string name)
            : base(context)
        {
            _ctx = (ProgressionContext)context;

            _headerCtx = new ProgressionHeaderContext
            {
                Name = name,
                ProgressionContext = _ctx
            };

            _header = new IElement<ProgressionHeaderContext>[]
            {
                new ProgressionHeaderBackgroundElement(),
                new ProgressionHeaderBordersElement(),
                new ProgressionIconsElement(),
                new ProgressionHeaderTitleElement()
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