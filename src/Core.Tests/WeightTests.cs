using Core.Exceptions;

namespace Core.Tests;

public class WeightTests
{
    [Theory]
    [MemberData(nameof(GetInvalidInputData))]
    public void Ctor_InvalidInput_ThrowsModelException(int input)
    {
        var actual = () =>
        {
            _ = new Weight(input);
        };

        actual.Should().Throw<ModelException>();
    }

    [Theory]
    [InlineData(1, 1, true)]
    [InlineData(1, 2, false)]
    public void OperatorEquals_CompareWeights_ReturnsSuccess(int leftValue, int rightValue, bool expected)
    {
        var leftWeight = new Weight(leftValue);
        var rightWeight = new Weight(rightValue);

        var actual = leftWeight == rightWeight;

        actual.Should().Be(expected);
    }

    [Fact]
    public void GetHashCode_NoInput_CreatesHash()
    {
        var vertex = new Weight(1);

        var actual = () =>
        {
            _ = vertex.GetHashCode();
        };

        actual.Should().NotThrow();
    }

    public static IEnumerable<object[]> GetInvalidInputData() =>
        new[]
        {
            new object[] { int.MinValue },
            new object[] { Weight.WeightMin - 1 },
            new object[] { Weight.WeightMax + 1 },
            new object[] { int.MaxValue }
        };
}