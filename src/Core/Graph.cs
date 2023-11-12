using Core.Abstractions;
using Core.Exceptions;

namespace Core;

/// <summary>
/// Object represents graph
/// </summary>
/// <typeparam name="T">Generic type of interface IEdge</typeparam>
public sealed class Graph<T> where T : IEdge
{
    /// <summary>
    /// Max. limit of graph edges
    /// </summary>
    public const int MaxEdges = 100;

    private readonly HashSet<T> _edges = new HashSet<T>();

    private GraphName _name;

    /// <summary>
    /// Parametrized ctor
    /// </summary>
    /// <param name="name">Graph name</param>
    /// <exception cref="ArgumentNullException">Parameter cannot be null</exception>
    public Graph(GraphName name)
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Graph name
    /// </summary>
    public GraphName Name => _name;

    /// <summary>
    /// Method creates snapshot from graph which can be stored
    /// </summary>
    /// <returns>Returns snapshot from graph</returns>
    public SnapshotGraph<T> ToSnapshot() =>
        new SnapshotGraph<T>(Name, _edges.ToArray());

    /// <summary>
    /// Method replicates graph from snapshot
    /// </summary>
    /// <param name="snapshot">A snapshot used for replication</param>
    /// <returns>Returns graph replicated from snapshot</returns>
    /// <exception cref="ArgumentNullException">Parameter cannot be null</exception>
    public static Graph<T> FromSnapshot(SnapshotGraph<T> snapshot)
    {
        if (snapshot is null)
        {
            throw new ArgumentNullException(nameof(snapshot));
        }

        var graph = new Graph<T>(new GraphName(snapshot.Name));
        foreach (var edge in snapshot.Edges)
        {
            graph.UpsertEdge(edge);
        }

        return graph;
    }

    /// <summary>
    /// Method for graph rename
    /// </summary>
    /// <param name="name">New name of graph</param>
    /// <exception cref="ArgumentNullException">Parameter cannot be null</exception>
    public void Rename(GraphName name) =>
        _name = name ?? throw new ArgumentNullException(nameof(name));

    /// <summary>
    /// Method create or update an edge in graph
    /// </summary>
    /// <param name="edge">New edge</param>
    /// <exception cref="ArgumentNullException">Parameter cannot be null</exception>
    /// <exception cref="ModelException">Custom exception for model validation</exception>
    public void UpsertEdge(T edge)
    {
        if (edge == null)
        {
            throw new ArgumentNullException(nameof(edge));
        }

        if (_edges.Count >= MaxEdges)
        {
            throw new ModelException($"You have exceeded maximum number ({MaxEdges}) of edges in graph");
        }

        TryDeleteEdge(edge);

        _edges.Add(edge);
    }

    /// <summary>
    /// Method delete existed edge
    /// </summary>
    /// <param name="edge">Edge to be deleted</param>
    /// <exception cref="ArgumentNullException">Parameter cannot be null</exception>
    public void TryDeleteEdge(T edge)
    {
        if (edge == null)
        {
            throw new ArgumentNullException(nameof(edge));
        }

        if (!_edges.Contains(edge))
        {
            return;
        }

        _edges.Remove(edge);
    }

    /// <summary>
    /// Method returns all edges of the graph
    /// </summary>
    /// <returns>Returns collection of all edges</returns>
    public IEnumerable<T> GetEdges() => _edges.ToArray();

    /// <summary>
    /// Method returns all vertices of the graph
    /// </summary>
    /// <returns>Returns collection of all vertices</returns>
    public IEnumerable<Vertex> GetVertices()
    {
        var vertices = new List<Vertex>();
        foreach (var edge in _edges)
        {
            vertices.Add(edge.Source);
            vertices.Add(edge.Destination);
        }

        return vertices.Distinct();
    }

    /// <summary>
    /// Method switch vertices, e.g. (A, B) to (B, A)
    /// </summary>
    /// <param name="edge">Edge where vertices will be switched</param>
    /// <exception cref="ArgumentNullException">Parameter cannot be null</exception>
    public void SwitchVertices(T edge)
    {
        if (edge == null)
        {
            throw new ArgumentNullException(nameof(edge));
        }

        UpsertEdge((T)edge.SwitchVertices());
    }
}