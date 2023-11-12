using System.Text.Json;
using Core.Abstractions;
using Core.Exceptions;

namespace Core;

/// <summary>
/// An object to store graph as json file under root folder ./SavedGraphs
/// </summary>
/// <typeparam name="T">Generic type that implements IEdge interface</typeparam>
public sealed class GraphJsonFileStorage<T> : IGraphStorage<T> where T : IEdge
{
    private const string FolderName = "SavedGraphs";
    private const string GraphNameSeparator = " ";
    private const string FileNameSeparator = "_";

    /// <summary>
    /// Async method create or update graph snapshot json file
    /// </summary>
    /// <param name="snapshot">Snapshot of graph</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns Task</returns>
    /// <exception cref="ArgumentNullException">Parameter cannot be null</exception>
    public Task UpsertAsync(SnapshotGraph<T> snapshot, CancellationToken cancellationToken = default)
    {
        if (snapshot is null)
        {
            throw new ArgumentNullException(nameof(snapshot));
        }

        var filePath = GetFilePath(new GraphName(snapshot.Name));

        return UpsertAsync(snapshot, filePath, cancellationToken);
    }

    /// <summary>
    /// Async method returns snapshot of graph from json file
    /// </summary>
    /// <param name="name">Graph name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns snapshot of graph</returns>
    /// <exception cref="ArgumentNullException">Parameter cannot be null</exception>
    public Task<SnapshotGraph<T>> GetAsync(GraphName name, CancellationToken cancellationToken = default)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        var filePath = GetFilePath(name);

        return GetAsync(filePath, cancellationToken);
    }

    /// <summary>
    /// Async method returns graph names of all saved json files under root folder ./SavedGraphs
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns graph names</returns>
    public Task<IEnumerable<GraphName>> GetAllGraphNamesAsync(CancellationToken cancellationToken = default) =>
        Directory.Exists(GetDirectoryPath())
            ? Task.FromResult(Directory.GetFiles(GetDirectoryPath()).Select(GetGraphNameFromFileName))
            : Task.FromResult(Enumerable.Empty<GraphName>());

    /// <summary>
    /// Async method deletes snapshot json file by graph name
    /// </summary>
    /// <param name="name">Graph name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Returns Task</returns>
    /// <exception cref="ArgumentNullException">Parameter cannot be null</exception>
    /// <exception cref="ModelException">Custom exception for model validation</exception>
    public Task DeleteAsync(GraphName name, CancellationToken cancellationToken = default)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        try
        {
            File.Delete(GetFilePath(name));
        }
        catch (Exception)
        {
            throw new ModelException("File cannot be deleted");
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Async method returns state if file exists
    /// </summary>
    /// <param name="name">Graph name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Return value true if file already exists otherwise false</returns>
    /// <exception cref="ArgumentNullException">Parameter cannot be null</exception>
    public Task<bool> ExistsAsync(GraphName name, CancellationToken cancellationToken = default) =>
        name != null
            ? Task.FromResult(File.Exists(GetFilePath(name)))
            : throw new ArgumentNullException(nameof(name));

    private Task UpsertAsync(SnapshotGraph<T> snapshot, string filePath, CancellationToken cancellationToken = default)
    {
        if (snapshot is null)
        {
            throw new ArgumentNullException(nameof(snapshot));
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        try
        {
            TryCreateDirectory();
            var json = JsonSerializer.Serialize(snapshot);
            File.WriteAllText(filePath, json);
        }
        catch (Exception)
        {
            throw new ModelException("File cannot be saved");
        }

        return Task.CompletedTask;
    }

    private Task<SnapshotGraph<T>> GetAsync(string filePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        string fileContent;
        try
        {
            fileContent = File.ReadAllText(filePath);
        }
        catch (Exception)
        {
            throw new ModelException($"File cannot be opened in path: {filePath}");
        }

        SnapshotGraph<T> snapshot;
        try
        {
            snapshot = JsonSerializer.Deserialize<SnapshotGraph<T>>(fileContent)!;
        }
        catch (Exception)
        {
            throw new ModelException("File doesn't meet required structure");
        }

        return Task.FromResult(snapshot);
    }

    private void TryCreateDirectory()
    {
        var directoryPath = GetDirectoryPath();
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    private string GetFileNameFromGraphName(GraphName name)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (Path.GetInvalidFileNameChars().Any(invalidChar => name.Value.Contains(invalidChar)))
        {
            throw new ModelException("Graph name should consists of only alphanumeric characters");
        }

        return $"{name.Value.Replace(GraphNameSeparator, FileNameSeparator)}.json";
    }

    private GraphName GetGraphNameFromFileName(string name) =>
        !string.IsNullOrWhiteSpace(name)
            ? new GraphName(Path.GetFileNameWithoutExtension(name).Replace(FileNameSeparator, GraphNameSeparator))
            : throw new ArgumentNullException(nameof(name));

    private string GetDirectoryPath() =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FolderName);

    private string GetFilePath(GraphName name) =>
        name != null
            ? Path.Combine(GetDirectoryPath(), GetFileNameFromGraphName(name))
            : throw new ArgumentNullException(nameof(name));
}