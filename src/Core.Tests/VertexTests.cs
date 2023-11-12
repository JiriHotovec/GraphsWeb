using Core.Exceptions;
using FluentAssertions;

namespace Core.Tests;

public class VertexTests
{
    [Theory]
    [MemberData(nameof(GetInvalidInputData))]
    public void Ctor_InvalidInput_ThrowsModelException(string input)
    {
        var actual = () => { _ = new Vertex(input); };

        actual.Should().Throw<ModelException>();
    }

    [Theory]
    [InlineData("Name1", "name1", true)]
    [InlineData("Name1", "name2", false)]
    public void OperatorEquals_CompareNames_ReturnsSuccess(string leftValue, string rightValue, bool expected)
    {
        var leftName = new Vertex(leftValue);
        var rightName = new Vertex(rightValue);

        var actual = leftName == rightName;

        actual.Should().Be(expected);
    }

    [Fact]
    public void GetHashCode_NoInput_CreatesHash()
    {
        var vertex = new Vertex("Name");

        var actual = () => { _ = vertex.GetHashCode(); };

        actual.Should().NotThrow();
    }

    [Fact]
    public void GetHashCode_SameValues_CreatesSameHash()
    {
        const string name = "Name";
        var vertex1 = new Vertex(name.ToLower());
        var vertex2 = new Vertex(name.ToUpper());

        vertex1.GetHashCode().Should().Be(vertex2.GetHashCode());
    }

    public static IEnumerable<object[]> GetInvalidInputData() =>
        new[]
        {
            new object[] { null! },
            new object[] { string.Empty },
            new object[] { new string('A', Vertex.NameMaxLength + 1) }
        };
}