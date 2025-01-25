// Token.cs
using RevStar.Foundation;

namespace RevStar.TokenSet;

// TokenStorage.cs
public class TokenStorage
{
    public void SaveTokens(IEnumerable<Token> tokens, string filePath)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(tokens);
        File.WriteAllText(filePath, json);
    }

    public IEnumerable<Token> LoadTokens(string filePath)
    {
        if (!File.Exists(filePath)) return Enumerable.Empty<Token>();
        var json = File.ReadAllText(filePath);
        return System.Text.Json.JsonSerializer.Deserialize<List<Token>>(json) ?? new List<Token>();
    }
}
