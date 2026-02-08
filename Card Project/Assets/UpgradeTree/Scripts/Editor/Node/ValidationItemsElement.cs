//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
namespace Eiquif.UpgradeTree.Editor
{
    public sealed class ValidationItemsElement : IElement<ValidationCtx>
    {
        private readonly IElement<ValidationItemData> _item = new CreateValidationItem();

        public void Execute(ValidationCtx ctx)
        {
            if (ctx == null) return;

            if (ctx.NullCount > 0)
                _item.Execute(new ValidationItemData
                {
                    Text = $"{ctx.NullCount} empty slot(s)",
                    Color = EditorColors.ErrorColor
                });

            if (ctx.NoIdCount > 0)
                _item.Execute(new ValidationItemData
                {
                    Text = $"{ctx.NoIdCount} node(s) without ID",
                    Color = EditorColors.WarningColor
                });

            if (ctx.DuplicateCount > 0)
                _item.Execute(new ValidationItemData
                {
                    Text = $"{ctx.DuplicateCount} duplicate ID(s) found",
                    Color = EditorColors.WarningColor
                });
        }
    }
}