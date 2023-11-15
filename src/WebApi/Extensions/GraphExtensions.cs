using Core;
using Core.Abstractions;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace WebApi.Extensions;

public static class GraphExtensions
{
    public static IServiceCollection AddGraph(this IServiceCollection sc)
    {
        ArgumentNullException.ThrowIfNull(sc);

        sc.TryAddScoped<IGraphStorage<WeightedEdge>, GraphJsonFileStorage<WeightedEdge>>();

        return sc;
    }
}