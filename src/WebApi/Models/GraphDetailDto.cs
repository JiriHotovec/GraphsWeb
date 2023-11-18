using Core;

namespace WebApi.Models;

public sealed record GraphDetailDto(string Name, IEnumerable<WeightedEdge> Edges);