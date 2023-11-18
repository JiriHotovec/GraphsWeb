using Core;

namespace WebApi.Models;

public sealed record GraphDetailUpdateDto(IEnumerable<WeightedEdge> Edges);