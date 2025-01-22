using System.Text.RegularExpressions;

namespace RevStar.Loader;

/// <summary>
/// A minimal data cleaner that trims whitespace, optionally lowercases,
/// and removes punctuation using a simple regex.
/// </summary>
public class BasicDataCleaner : IDataCleaner
{
    private readonly bool _convertToLower;
    private static readonly Regex _punctRegex = new Regex("[^\\w\\s]", RegexOptions.Compiled);

    public BasicDataCleaner(bool convertToLower = true)
    {
        _convertToLower = convertToLower;
    }

    /// <summary>
    /// Cleans or normalizes a single line: trims whitespace, removes punctuation,
    /// optionally lowercases.
    /// </summary>
    /// <param name="rawLine">Raw text to be cleaned.</param>
    /// <returns>Cleaned version of the line.</returns>
    public string CleanLine(string rawLine)
    {
        if (string.IsNullOrWhiteSpace(rawLine))
            return string.Empty;

        string trimmed = rawLine.Trim();

        if (_convertToLower)
        {
            trimmed = trimmed.ToLowerInvariant();
        }

        // Remove punctuation (non-word characters, except spaces)
        string cleaned = _punctRegex.Replace(trimmed, "");

        return cleaned;
    }
}
