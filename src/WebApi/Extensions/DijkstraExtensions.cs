using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApi.Services;
using WebApi.Services.Abstractions;

namespace WebApi.Extensions;

public static class DijkstraExtensions
{
    public static IServiceCollection AddDijkstra(this IServiceCollection sc)
    {
        ArgumentNullException.ThrowIfNull(sc);

        sc.TryAddScoped<IDijkstraService, DijkstraService>();

        return sc;
    }
}