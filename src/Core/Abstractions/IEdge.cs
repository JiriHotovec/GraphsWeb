namespace Core.Abstractions;

/// <summary>
/// An interface that represents edge object
/// </summary>
public interface IEdge
{
    /// <summary>
    /// Vertex source
    /// </summary>
    Vertex Source { get; }

    /// <summary>
    /// Vertex destination
    /// </summary>
    Vertex Destination { get; }

    /// <summary>
    /// Method to switch edge's vertices
    /// </summary>
    /// <returns>Returns IEdge interface</returns>
    IEdge SwitchVertices();

    /// <summary>
    /// Method returns if relation to the edge exists
    /// </summary>
    /// <param name="other">Edge to compare</param>
    /// <returns>Returns value true if relation between edges exist</returns>
    bool HasRelation(IEdge other);
}