// Token.cs
using RevStar.Foundation;

namespace RevStar.TokenSet;

// TokenSorter.cs
public static class TokenSorter
{
    public static IEnumerable<Token> SortByValue(IEnumerable<Token> tokens, bool ascending = true)
    {
        return ascending
            ? tokens.OrderBy(t => t.Value)
            : tokens.OrderByDescending(t => t.Value);
    }

    public static IEnumerable<Token> SortByFrequency(IEnumerable<Token> tokens, bool ascending = true)
    {
        return ascending
            ? tokens.OrderBy(t => t.Frequency)
            : tokens.OrderByDescending(t => t.Frequency);
    }

    public static IEnumerable<Token> SortByCustom(IEnumerable<Token> tokens, 
                         Func<Token, object> keySelector, bool ascending = true)
    {
        return ascending
            ? tokens.OrderBy(keySelector)
            : tokens.OrderByDescending(keySelector);
    }
}
