using Core.Abstractions;

namespace Dijkstra.Abstractions;

/// <summary>
/// An interface for Dijkstra's provider returned path result
/// </summary>
public interface IPathResult
{
    bool IsSuccess { get; }

    IEnumerable<IWeightedEdge> Paths { get; }
}