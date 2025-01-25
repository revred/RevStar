// Token.cs
using RevStar.Foundation;

namespace RevStar.TokenSet;

// TokenSetManager.cs
public class TokenSetManager
{
    private readonly HashSet<Token> _tokens = new();

    public void AddToken(Token token)
    {
        _tokens.Add(token);
    }

    public bool RemoveToken(string value)
    {
        return _tokens.RemoveWhere(t => t.Value == value) > 0;
    }

    public Token? FindToken(string value)
    {
        return _tokens.FirstOrDefault(t => t.Value == value);
    }

    public IEnumerable<Token> SearchTokens(Func<Token, bool> predicate)
    {
        return _tokens.Where(predicate);
    }

    public void ClearTokens()
    {
        _tokens.Clear();
    }

    public IEnumerable<Token> GetAllTokens()
    {
        return _tokens;
    }
}
