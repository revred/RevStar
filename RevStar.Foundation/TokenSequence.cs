namespace RevStar.Foundation;

/// <summary>
/// Represents a tokenized sequence (e.g., a sentence or segment),
/// storing the integer IDs for each token.
/// </summary>
public class TokenSequence
{
    /// <summary>
    /// The token IDs that make up this sequence.
    /// </summary>
    public IReadOnlyList<int> Tokens { get; }

    /// <summary>
    /// Length of the sequence in tokens.
    /// </summary>
    public int Length => Tokens.Count;

    public TokenSequence(IReadOnlyList<int> tokens)
    {
        Tokens = tokens;
    }
}
