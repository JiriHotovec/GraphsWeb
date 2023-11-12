using Core.Abstractions;

namespace Core;

/// <summary>
/// Snapshot of specific graph
/// </summary>
/// <typeparam name="T">Generic type of IEdge interface</typeparam>
public sealed class SnapshotGraph<T> where T : IEdge
{
    /// <summary>
    /// Parametrized ctor
    /// </summary>
    /// <param name="name">Graph name</param>
    /// <param name="edges">Graph edges</param>
    /// <exception cref="ArgumentNullException">Parameter cannot be null</exception>
    public SnapshotGraph(string name, IEnumerable<T> edges)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        Name = name;
        Edges = edges ?? throw new ArgumentNullException(nameof(edges));
    }

    /// <summary>
    /// Graph name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Graph edges
    /// </summary>
    public IEnumerable<T> Edges { get; }
}