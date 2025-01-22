namespace RevStar.Foundation;

/// <summary>
/// Defines the contract for mapping between text and integer token IDs.
/// </summary>
public interface ITokenizer
{
    /// <summary>
    /// Converts a raw string into a sequence of token IDs.
    /// </summary>
    /// <param name="text">The input text to tokenize.</param>
    /// <returns>A list of integer token IDs.</returns>
    IReadOnlyList<int> TokenizeText(string text);

    /// <summary>
    /// Converts token IDs back to text (detokenization).
    /// </summary>
    /// <param name="tokens">The token IDs to convert.</param>
    /// <returns>A string representing the reconstructed text.</returns>
    string Detokenize(IReadOnlyList<int> tokens);

    /// <summary>
    /// Builds or updates an internal vocabulary given a corpus.
    /// May be optional depending on the implementation.
    /// </summary>
    void BuildVocabulary(IEnumerable<string> corpus);

    /// <summary>
    /// Exposes the current vocabulary size.
    /// </summary>
    int VocabSize { get; }
}
