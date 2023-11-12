using Core.Exceptions;

namespace Core;

/// <summary>
/// Immutable value object that represents edge's weight
/// </summary>
public sealed class Weight : IEquatable<Weight>, IComparable<Weight>, IComparable
{
    /// <summary>
    /// Min. limit of weight value
    /// </summary>
    public const int WeightMin = 0;

    /// <summary>
    /// Max. limit of weight value
    /// </summary>
    public const int WeightMax = 1000;

    /// <summary>
    /// Parametrized ctor
    /// </summary>
    /// <param name="value">Weight value</param>
    /// <exception cref="ModelException">Custom exception for model validation</exception>
    public Weight(int value = WeightMin)
    {
        if (value < WeightMin || value > WeightMax)
        {
            throw new ModelException($"Weight must be in interval <{WeightMin}, {WeightMax}>");
        }

        Value = value;
    }

    /// <summary>
    /// Weight value
    /// </summary>
    public int Value { get; }

    /// <inheritdoc />
    public override string ToString() => $"{Value}";

    public static implicit operator int(Weight weight)
    {
        return weight.Value;
    }

    /// <inheritdoc />
    public bool Equals(Weight? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is Weight other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Value;
    }

    public static bool operator ==(Weight? left, Weight? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Weight? left, Weight? right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc />
    public int CompareTo(Weight? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return Value.CompareTo(other.Value);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is Weight other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(Weight)}");
    }

    public static bool operator <(Weight? left, Weight? right)
    {
        return Comparer<Weight>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(Weight? left, Weight? right)
    {
        return Comparer<Weight>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(Weight? left, Weight? right)
    {
        return Comparer<Weight>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(Weight? left, Weight? right)
    {
        return Comparer<Weight>.Default.Compare(left, right) >= 0;
    }
}