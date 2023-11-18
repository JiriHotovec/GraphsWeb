using Core;
using WebApi.Models;

namespace WebApi.Services.Abstractions;

public interface IGraphService
{
    Task<IEnumerable<GraphDto>> GetGraphs(CancellationToken cancellationToken = default);

    Task<GraphDetailDto?> GetGraph(GraphName name, CancellationToken cancellationToken = default);

    Task<GraphDetailDto> UpsertGraph(GraphDetailDto graph, CancellationToken cancellationToken = default);

    Task DeleteGraph(GraphName name, CancellationToken cancellationToken = default);

    Task<bool> ExistsGraph(GraphName name, CancellationToken cancellationToken = default);
}