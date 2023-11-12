using Core;

namespace Dijkstra.Abstractions;

/// <summary>
/// An interface for Dijkstra's provider
/// </summary>
public interface IDijkstraProvider
{
    Task<IPathResult> GetShortestPathAsync(
        Vertex source,
        Vertex destination,
        CancellationToken cancellationToken = default);
}