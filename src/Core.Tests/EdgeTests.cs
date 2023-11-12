using Core.Exceptions;

namespace Core.Tests;

public class EdgeTests
{
    [Theory]
    [MemberData(nameof(GetValidInputData))]
    public void Ctor_ValidInput_ReturnsSameValues(Vertex input1, Vertex input2, Edge expected)
    {
        var actual = new Edge(input1, input2);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Ctor_InvalidInput_ThrowsModelException()
    {
        const string sameName = "SameName";

        var actual = () =>
        {
            _ = new Edge(new Vertex(sameName), new Vertex(sameName));
        };

        actual.Should().Throw<ModelException>();
    }

    [Theory]
    [MemberData(nameof(GetEqualityData))]
    public void OperatorEquals_CompareEdges_ReturnsSuccess(Edge leftValue, Edge rightValue, bool expected)
    {
        var actual = leftValue == rightValue;

        actual.Should().Be(expected);
    }

    [Fact]
    public void GetHashCode_NoInput_CreatesHash()
    {
        var vertex = new Edge(new Vertex("Name1"), new Vertex("Name2"));

        var actual = () =>
        {
            _ = vertex.GetHashCode();
        };

        actual.Should().NotThrow();
    }

    [Fact]
    public void GetHashCode_SameValues_CreatesSameHash()
    {
        var edge1 = new Edge(new Vertex("Name1"), new Vertex("Name2"));
        var edge2 = new Edge(new Vertex("Name2"), new Vertex("Name1"));

        edge1.GetHashCode().Should().Be(edge2.GetHashCode());
    }

    [Fact]
    public void SwitchVertices_Input_ReturnsEdge()
    {
        var edge = new Edge(new Vertex("Name1"), new Vertex("Name2"));
        var expected = new Edge(new Vertex("Name2"), new Vertex("Name1"));

        var actual = edge.SwitchVertices();

        actual.Source.Should().Be(expected.Source);
        actual.Destination.Should().Be(expected.Destination);
    }

    [Theory]
    [MemberData(nameof(GetHasRelationData))]
    public void HasRelation_EdgeInput_Returns(Edge left, Edge right, bool expected)
    {
        var actual = left.HasRelation(right);

        actual.Should().Be(expected);
    }

    public static IEnumerable<object[]> GetValidInputData() =>
        new[]
        {
            new object[]
            {
                new Vertex("Name1"),
                new Vertex("Name2"),
                new Edge(new Vertex("Name1"), new Vertex("Name2"))
            },
            new object[]
            {
                new Vertex("Name2"),
                new Vertex("Name1"),
                new Edge(new Vertex("Name2"), new Vertex("Name1"))
            }
        };

    public static IEnumerable<object[]> GetEqualityData() =>
        new[]
        {
            new object[]
            {
                new Edge(new Vertex("Name1"), new Vertex("Name2")),
                new Edge(new Vertex("Name1"), new Vertex("Name2")),
                true
            },
            new object[]
            {
                new Edge(new Vertex("Name1"), new Vertex("Name2")),
                new Edge(new Vertex("Name2"), new Vertex("Name1")),
                true
            },
            new object[]
            {
                new Edge(new Vertex("Name1"), new Vertex("Name2")),
                new Edge(new Vertex("Name1"), new Vertex("Name3")),
                false
            },
            new object[]
            {
                new Edge(new Vertex("Name1"), new Vertex("Name2")),
                new Edge(new Vertex("Name3"), new Vertex("Name2")),
                false
            }
        };

    public static IEnumerable<object[]> GetHasRelationData() =>
        new[]
        {
            new object[]
            {
                new Edge(new Vertex("Name1"), new Vertex("Name2")),
                new Edge(new Vertex("Name2"), new Vertex("Name1")),
                true
            },
            new object[]
            {
                new Edge(new Vertex("Name1"), new Vertex("Name2")),
                new Edge(new Vertex("Name1"), new Vertex("Name2")),
                true
            },
            new object[]
            {
                new Edge(new Vertex("Name1"), new Vertex("Name2")),
                new Edge(new Vertex("Name2"), new Vertex("Name3")),
                true
            },
            new object[]
            {
                new Edge(new Vertex("Name1"), new Vertex("Name2")),
                new Edge(new Vertex("Name3"), new Vertex("Name4")),
                false
            },
        };
}