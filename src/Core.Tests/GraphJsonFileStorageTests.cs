namespace Core.Tests;

public class GraphJsonFileStorageTests
{
    [Fact]
    public async Task FileWorkflow_Edge()
    {
        var graphName = new GraphName(Guid.NewGuid().ToString());
        var snapshot = new SnapshotGraph<Edge>(
            graphName.Value,
            new[]
            {
                new Edge(new Vertex("A"), new Vertex("B")),
                new Edge(new Vertex("B"), new Vertex("C")),
            });

        var storage = new GraphJsonFileStorage<Edge>();
        await storage.UpsertAsync(snapshot);

        (await storage.ExistsAsync(graphName)).Should().Be(true);
        (await storage.GetAllGraphNamesAsync()).Should().HaveCount(1);
        (await storage.GetAsync(graphName)).Should().BeEquivalentTo(snapshot);
        await storage.DeleteAsync(graphName);
        (await storage.ExistsAsync(graphName)).Should().Be(false);
    }
}