// Token.cs
using RevStar.Foundation;

namespace RevStar.TokenSet;

// TokenStatistics.cs
public class TokenStatistics
{
    public Token? GetMostFrequentToken(IEnumerable<Token> tokens)
    {
        return tokens.OrderByDescending(t => t.Frequency).FirstOrDefault();
    }

    public IEnumerable<Token> GetUniqueTokens(IEnumerable<Token> tokens)
    {
        return tokens.GroupBy(t => t.Value).Where(g => g.Count() == 1).Select(g => g.First());
    }

    public int GetTotalTokenCount(IEnumerable<Token> tokens)
    {
        return tokens.Count();
    }

    public Dictionary<string, int> GetTokenFrequencyDistribution(IEnumerable<Token> tokens)
    {
        return tokens.GroupBy(t => t.Value).ToDictionary(g => g.Key, g => g.Sum(t => t.Frequency));
    }
}
