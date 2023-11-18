using Core;

namespace WebApi.Services.Abstractions;

public interface IDijkstraService
{
    Task<IEnumerable<WeightedEdge>> GetShortestPath(
        GraphName name,
        Vertex source,
        Vertex destination,
        CancellationToken cancellationToken = default);
}