// Token.cs
using RevStar.Foundation;

namespace RevStar.TokenSet;

// TokenFilter.cs
public static class TokenFilter
{
    public static IEnumerable<Token> FilterByPattern(IEnumerable<Token> tokens, string pattern)
    {
        var regex = new System.Text.RegularExpressions.Regex(pattern);
        return tokens.Where(t => regex.IsMatch(t.Value));
    }

    public static IEnumerable<Token> FilterByMetadata(IEnumerable<Token> tokens, string key, object value)
    {
        return tokens.Where(t => t.Metadata.ContainsKey(key) && t.Metadata[key].Equals(value));
    }
}
