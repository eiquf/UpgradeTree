namespace Eiquif.UpgradeTree.Editor.TreeWindow
{
#nullable enable
    interface IElement<T> where T : class
    {
        void Execute(T? t);
    }

    interface IElement
    {
        void Execute();
    }
}