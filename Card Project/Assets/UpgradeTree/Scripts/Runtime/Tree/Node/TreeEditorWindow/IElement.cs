#nullable enable
/// <summary>
/// UI element that participates in rendering one list item.
/// USes as step of Draw pipeline
/// </summary>
public interface IElement<T> where T : class
{
    /// <summary>
    /// Draw element
    /// </summary>
    void Execute(T? context);
}

/// <summary>
/// UI element without context
/// </summary>
public interface IElement
{
    void Execute();
}