namespace Core.Abstractions;

/// <summary>
/// An interface to store a graph
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGraphStorage<T> where T : IEdge
{
    /// <summary>
    /// Async method create or update graph snapshot
    /// </summary>
    /// <param name="snapshot">Snapshot of graph</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns Task</returns>
    Task UpsertAsync(SnapshotGraph<T> snapshot, CancellationToken cancellationToken = default);

    /// <summary>
    /// Async method returns snapshot of graph
    /// </summary>
    /// <param name="name">Graph name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns snapshot of graph</returns>
    Task<SnapshotGraph<T>> GetAsync(GraphName name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Async method returns all graph names for stored graphs
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns graph names</returns>
    Task<IEnumerable<GraphName>> GetAllGraphNamesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Async method deletes snapshot by graph name
    /// </summary>
    /// <param name="name">Graph name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns Task</returns>
    Task DeleteAsync(GraphName name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Async method returns state if graph exists
    /// </summary>
    /// <param name="name">Graph name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Return value true if graph already exists otherwise false</returns>
    Task<bool> ExistsAsync(GraphName name, CancellationToken cancellationToken = default);
}