// Token.cs
using RevStar.Foundation;

namespace RevStar.TokenSet;

// TokenSetValidator.cs
public class TokenSetValidator
{
    public bool ValidateToken(Token token)
    {
        return !string.IsNullOrEmpty(token.Value);
    }

    public IEnumerable<Token> RemoveDuplicates(IEnumerable<Token> tokens)
    {
        return tokens.GroupBy(t => t.Value).Select(g => g.First());
    }

    public bool IsValidSet(IEnumerable<Token> tokens)
    {
        return tokens.All(ValidateToken);
    }
}
