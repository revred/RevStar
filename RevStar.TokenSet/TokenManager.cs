

namespace RevStar.TokenSet;

public class TokenManager
{
    private readonly Dictionary<string, int> _tokenToId;
    private readonly Dictionary<int, string> _idToToken;

    public TokenManager()
    {
        // Example initialization of vocabulary (replace with actual tokenizer data).
        _tokenToId = new Dictionary<string, int>
        {
            {"<PAD>", 0},
            {"<UNK>", 1},
            {"hello", 2},
            {"world", 3},
            {"this", 4},
            {"is", 5},
            {"a", 6},
            {"test", 7}
        };

        // Reverse mapping for decoding.
        _idToToken = _tokenToId.ToDictionary(kv => kv.Value, kv => kv.Key);
    }

    /// <summary>
    /// Encodes the input text into a list of token IDs.
    /// </summary>
    /// <param name="text">The input text to encode.</param>
    /// <returns>A list of token IDs representing the input text.</returns>
    public List<int> Encode(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Input text cannot be null or empty.", nameof(text));

        var tokens = text.Split(' '); // Simple whitespace tokenizer (replace with advanced tokenization if needed).
        var tokenIds = new List<int>();

        foreach (var token in tokens)
        {
            if (_tokenToId.TryGetValue(token, out int tokenId))
            {
                tokenIds.Add(tokenId);
            }
            else
            {
                tokenIds.Add(_tokenToId["<UNK>"]); // Use <UNK> for unknown tokens.
            }
        }

        return tokenIds;
    }

    /// <summary>
    /// Decodes a list of token IDs back into a string.
    /// </summary>
    /// <param name="tokenIds">The list of token IDs to decode.</param>
    /// <returns>A string representation of the decoded token IDs.</returns>
    public string Decode(List<int> tokenIds)
    {
        if (tokenIds == null || tokenIds.Count == 0)
            throw new ArgumentException("Token IDs cannot be null or empty.", nameof(tokenIds));

        var tokens = tokenIds.Select(id => _idToToken.ContainsKey(id) ? _idToToken[id] : "<UNK>").ToList();
        return string.Join(" ", tokens);
    }
}
