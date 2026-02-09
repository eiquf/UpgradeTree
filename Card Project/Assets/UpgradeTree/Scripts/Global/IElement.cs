//***************************************************************************************
// Writer: Eiquif
// Last Updated: January 2026
//***************************************************************************************
#nullable enable
/// <summary>
/// UI element that participates in rendering one list item.
/// Uses as step of Draw pipeline
/// </summary>
public interface IElement<T>
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