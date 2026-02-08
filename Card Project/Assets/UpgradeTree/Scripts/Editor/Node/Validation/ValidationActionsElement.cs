//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
namespace Eiquif.UpgradeTree.Editor
{
    public sealed class ValidationActionsElement : IElement<ValidationCtx>
    {
        private readonly IElement<ValidationCtx> _clean =
            new CreateValidationCleanButton();

        private readonly IElement<ValidationCtx> _removeDup =
            new CreateValidationRemoveDupButton();

        public void Execute(ValidationCtx ctx)
        {
            if (ctx == null) return;

            if (ctx.NullCount > 0)
                _clean.Execute(ctx);

            if (ctx.DuplicateCount > 0)
                _removeDup.Execute(ctx);
        }
    }
}