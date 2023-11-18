using Core;
using Core.Abstractions;
using Dijkstra;
using WebApi.Services.Abstractions;

namespace WebApi.Services;

public sealed class DijkstraService : IDijkstraService
{
    private readonly IGraphStorage<WeightedEdge> _storage;

    public DijkstraService(IGraphStorage<WeightedEdge> storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public async Task<IEnumerable<WeightedEdge>> GetShortestPath(
        GraphName name,
        Vertex source,
        Vertex destination,
        CancellationToken cancellationToken = default)
    {
        if (!await _storage.ExistsAsync(name, cancellationToken))
        {
            return ArraySegment<WeightedEdge>.Empty;
        }

        var graphSnapshot = await _storage.GetAsync(name, cancellationToken);
        var graph = Graph<WeightedEdge>.FromSnapshot(graphSnapshot);

        var dijkstra = new DijkstraProvider(graph.GetEdges());
        var result = await dijkstra.GetShortestPathAsync(source, destination, cancellationToken);

        return result.Paths.Select(s => new WeightedEdge(new Vertex(s.Source), new Vertex(s.Destination), new Weight(s.Weight)));
    }
}