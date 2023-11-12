using Core.Abstractions;

namespace Core;

/// <summary>
/// Weighted edge (A, B) = (B, A)
/// </summary>
public sealed class WeightedEdge : Edge, IWeightedEdge
{
    /// <summary>
    /// Parametrized ctor
    /// </summary>
    /// <param name="source">Vertex source</param>
    /// <param name="destination">Vertex destination</param>
    /// <param name="weight">Edge's weight</param>
    /// <exception cref="ArgumentNullException">Parameter cannot be null</exception>
    public WeightedEdge(Vertex source, Vertex destination, Weight weight)
        : base(source, destination)
    {
        Weight = weight ?? throw new ArgumentNullException(nameof(weight));
    }

    /// <summary>
    /// Edge weight
    /// </summary>
    public Weight Weight { get; }

    /// <inheritdoc />
    public override string ToString() => $"({Source}, {Destination}) {Weight}";

    /// <inheritdoc />
    public override IEdge SwitchVertices() =>
        new WeightedEdge(Destination, Source, Weight);
}