using Core;
using Core.Abstractions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApi.Models;
using WebApi.Services;
using WebApi.Services.Abstractions;

namespace WebApi.Extensions;

public static class GraphExtensions
{
    public static IServiceCollection AddGraph(this IServiceCollection sc)
    {
        ArgumentNullException.ThrowIfNull(sc);

        sc.TryAddScoped<IGraphStorage<WeightedEdge>, GraphJsonFileStorage<WeightedEdge>>();
        sc.TryAddScoped<IGraphService, GraphService>();

        return sc;
    }

    public static GraphDetailDto ToDto(this Graph<WeightedEdge> graph)
    {
        var dto = new GraphDetailDto(graph.Name);

        return dto;
    }

    public static Graph<WeightedEdge> ToEntity(this GraphDetailDto dto)
    {
        var entity = new Graph<WeightedEdge>(new GraphName(dto.Name));

        return entity;
    }
}