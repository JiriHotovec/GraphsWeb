using Core.Exceptions;

namespace Core.Tests;

public class GraphTests
{
    [Fact]
    public void Ctor_NullInput_ThrowsArgumentNullException()
    {
        var actual = () =>
        {
            _ = new Graph<Edge>(null!);
        };

        actual.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void UpsertEdge_NullInput_ThrowsArgumentNullException()
    {
        var graph = new Graph<Edge>(new GraphName("Name"));

        var actual = () =>
        {
            graph.UpsertEdge(null!);
        };

        actual.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void UpsertEdge_MaxInputsExceeded_ThrowsModelException()
    {
        const int maxEdges = Graph<Edge>.MaxEdges;

        var graph = new Graph<Edge>(new GraphName("Name"));
        for (var i = 0; i < maxEdges; i++)
        {
            graph.UpsertEdge(new Edge(new Vertex("SameVertex"), new Vertex(i.ToString())));
        }

        var actual = () =>
        {
            graph.UpsertEdge(new Edge(new Vertex("SameVertex"), new Vertex("DiffVertex")));
        };

        actual.Should().Throw<ModelException>();
    }

    [Theory]
    [MemberData(nameof(GetValidEdges))]
    public void UpsertEdge_ValidEdges_ReturnsCount(IEnumerable<Edge> inputs, int expected)
    {
        var graph = new Graph<Edge>(new GraphName("Name"));
        foreach (var edge in inputs)
        {
            graph.UpsertEdge(edge);
        }

        graph.GetEdges().Should().HaveCount(c => c == expected);
    }

    [Fact]
    public void TryDelete_NullInput_ThrowsArgumentNullException()
    {
        var graph = new Graph<Edge>(new GraphName("Name"));

        var actual = () =>
        {
            graph.TryDeleteEdge(null!);
        };

        actual.Should().Throw<ArgumentNullException>();
    }

    public static IEnumerable<object[]> GetValidEdges() =>
        new[]
        {
            new object[]
            {
                Array.Empty<Edge>(),
                0
            },
            new object[]
            {
                new[]
                {
                    new Edge(new Vertex("A"), new Vertex("B"))
                },
                1
            },
            new object[]
            {
                new[]
                {
                    new Edge(new Vertex("A"), new Vertex("B")),
                    new Edge(new Vertex("B"), new Vertex("A"))
                },
                1
            },
            new object[]
            {
                new[]
                {
                    new Edge(new Vertex("A"), new Vertex("B")),
                    new Edge(new Vertex("A"), new Vertex("C")),
                    new Edge(new Vertex("A"), new Vertex("D")),
                },
                3
            }
        };
}