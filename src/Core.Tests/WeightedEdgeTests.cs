namespace Core.Tests;

public class WeightedEdgeTests
{
    [Theory]
    [MemberData(nameof(GetWeightInputData))]
    public void Ctor_WeightInput_ReturnsSuccess(Weight leftWeight, Weight rightWeight, bool expected)
    {
        const string srcVertexName = "Name1";
        const string dstVertexName = "Name2";
        var left = new WeightedEdge(new Vertex(srcVertexName), new Vertex(dstVertexName), leftWeight);
        var right = new WeightedEdge(new Vertex(srcVertexName), new Vertex(dstVertexName), rightWeight);

        var actual = left.Weight == right.Weight;

        actual.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(GetEqualityData))]
    public void OperatorEquals_CompareEdges_ReturnsSuccess(WeightedEdge leftValue, WeightedEdge rightValue, bool expected)
    {
        var actual = leftValue == rightValue;

        actual.Should().Be(expected);
    }

    public static IEnumerable<object[]> GetWeightInputData() =>
        new[]
        {
            new object[]
            {
                new Weight(1),
                new Weight(1),
                true
            },
            new object[]
            {
                new Weight(1),
                new Weight(2),
                false
            }
        };

    public static IEnumerable<object[]> GetEqualityData() =>
        new[]
        {
            new object[]
            {
                new WeightedEdge(new Vertex("Name1"), new Vertex("Name2"), new Weight(1)),
                new WeightedEdge(new Vertex("Name2"), new Vertex("Name1"), new Weight(1)),
                true
            },
            new object[]
            {
                new WeightedEdge(new Vertex("Name1"), new Vertex("Name2"), new Weight(1)),
                new WeightedEdge(new Vertex("Name1"), new Vertex("Name2"), new Weight(2)),
                true
            },
            new object[]
            {
                new WeightedEdge(new Vertex("Name1"), new Vertex("Name2"), new Weight(1)),
                new WeightedEdge(new Vertex("Name1"), new Vertex("Name3"), new Weight(1)),
                false
            }
        };
}