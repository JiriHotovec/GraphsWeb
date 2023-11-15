using Core;
using Core.Abstractions;
using WebApi.Extensions;
using WebApi.Models;
using WebApi.Services.Abstractions;

namespace WebApi.Services;

public sealed class GraphService : IGraphService
{
    private readonly IGraphStorage<WeightedEdge> _storage;

    public GraphService(IGraphStorage<WeightedEdge> storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public async Task<IEnumerable<GraphDto>> GetGraphs(CancellationToken cancellationToken = default)
    {
        var graphs = await _storage.GetAllGraphNamesAsync(cancellationToken);

        return graphs.Select(s => new GraphDto(s.Value));
    }

    public async Task<GraphDetailDto?> GetGraph(GraphName name, CancellationToken cancellationToken = default)
    {
        if (!await _storage.ExistsAsync(name, cancellationToken))
        {
            return null;
        }

        var graphSnapshot = await _storage.GetAsync(name, cancellationToken);

        return Graph<WeightedEdge>.FromSnapshot(graphSnapshot).ToDto();
    }

    public async Task<GraphDetailDto> UpsertGraph(GraphName name, GraphDetailDto graph, CancellationToken cancellationToken = default)
    {
        await _storage.UpsertAsync(graph.ToEntity().ToSnapshot(), cancellationToken);

        return graph;
    }

    public Task DeleteGraph(GraphName name, CancellationToken cancellationToken = default)
    {
        return _storage.DeleteAsync(name, cancellationToken);
    }

    public Task<bool> ExistsGraph(GraphName name, CancellationToken cancellationToken = default)
    {
        return _storage.ExistsAsync(name, cancellationToken);
    }
}