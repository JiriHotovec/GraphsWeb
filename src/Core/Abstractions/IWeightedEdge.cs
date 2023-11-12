namespace Core.Abstractions;

/// <summary>
/// An interface represents weighted edge
/// </summary>
public interface IWeightedEdge : IEdge
{
    /// <summary>
    /// Weight of the edge
    /// </summary>
    Weight Weight { get; }
}