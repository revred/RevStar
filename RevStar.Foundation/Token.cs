// Token.cs
namespace RevStar.Foundation;

public class Token : IComparable<Token>
{
    public string Value { get; set; } = string.Empty;
    public int Frequency { get; set; } = 0;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public Dictionary<string, object> Metadata { get; set; } = new();

    public int CompareTo(Token? other)
    {
        if (other == null) return 1;
        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj)
    {
        return obj is Token other && Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
