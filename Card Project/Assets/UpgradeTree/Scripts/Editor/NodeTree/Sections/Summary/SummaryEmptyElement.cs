//***************************************************************************************
// Author: Eiquif
// Last Updated: January 2026
//***************************************************************************************
namespace Eiquif.UpgradeTree.Editor
{
    public sealed class SummaryEmptyElement : IElement
    {
        public void Execute()
        {
            EditorEmptyStates.Draw(
                "📊",
                "No Data",
                "Add nodes to see statistics"
            );
        }
    }

}