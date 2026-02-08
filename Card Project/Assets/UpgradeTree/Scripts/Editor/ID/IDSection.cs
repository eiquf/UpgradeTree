using Eiquif.UpgradeTree.Runtime;
using System.Collections.Generic;
using UnityEditorInternal;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class IDSection : Section
    {
        private readonly NodeTreeContext _ctx;
        private ReorderableList _idList;
        private bool _show = true;

        private readonly List<IElement<NodeTreeContext>> _elements;

        public IDSection(ContextSystem ctx) : base(ctx)
        {
            _ctx = (NodeTreeContext)ctx;

            _elements = new()
            {
                new IDListSetupElement(list => _idList = list),
                new IDListDrawElement(() => _idList),
                new IDStatsElement()
            };
        }

        public override void Draw()
        {
            CollapsibleSection.Draw(
                "ID Database",
                "🆔",
                ref _show,
                EditorColors.PrimaryColor,
                () =>
                {
                    foreach (var e in _elements)
                        e.Execute(_ctx);
                });
        }
    }
}