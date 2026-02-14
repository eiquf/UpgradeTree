//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
using Eiquif.UpgradeTree.Runtime;
using System.Linq;
using UnityEditor;

namespace Eiquif.UpgradeTree.Editor
{
    public sealed class SummarySection : Section
    {
        private readonly NodeTreeContext _ctx;
        private readonly EditorFlowerAnimation _anim = new();

        private bool _show = true;

        private readonly IElement _empty = new SummaryEmptyElement();
        private readonly IElement<SummaryCtx> _stats = new SummaryStatsElement();
        private readonly IElement _distributionHeader = new SummaryDistributionHeaderElement();
        private readonly IElement<SummaryCtx> _rows = new SummaryDistributionRowsElement();

        public SummarySection(NodeTreeContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public override void Draw()
        {
            CollapsibleSection.Draw(
                "Summary",
                "📊",
                ref _show,
                EditorColors.InfoColor,
                DrawInternal
            );
        }

        private void DrawInternal()
        {
            var tree = _ctx.Tree;

            if (tree.Nodes == null || tree.Nodes.Count == 0)
            {
                _empty.Execute();
                return;
            }

            var ctx = BuildContext();

            _stats.Execute(ctx);
            _distributionHeader.Execute();
            _rows.Execute(ctx);
        }

        private SummaryCtx BuildContext()
        {
            var nodes = _ctx.Tree.Nodes.Where(n => n != null).ToList();

            var groups = nodes
                .GroupBy(n => string.IsNullOrEmpty(n.ID.Value) ? "[No ID]" : n.ID.Value)
                .OrderByDescending(g => g.Count())
                .Select(g => new SummaryGroup
                {
                    IdKey = g.Key,
                    Nodes = g.ToList(),
                    IsNoId = g.Key == "[No ID]",
                    HasDuplicates = g.Count() > 1 && g.Key != "[No ID]"
                })
                .ToList();

            return new SummaryCtx
            {
                TreeContext = _ctx,
                TotalNodes = nodes.Count,
                TotalIds = _ctx.Tree.IDs?.Count ?? 0,
                AssignedNodes = nodes.Count(n => !string.IsNullOrEmpty(n.ID.Value)),
                Groups = groups,
                Anim = _anim
            };
        }
    }
}