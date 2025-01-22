namespace RevStar.Loader;

/// <summary>
/// Defines operations for cleaning or transforming raw text lines 
/// into normalized forms suitable for further processing.
/// </summary>
public interface IDataCleaner
{
    /// <summary>
    /// Cleans or normalizes a single line of text.
    /// This may include trimming whitespace, lowercasing, 
    /// or removing unwanted symbols.
    /// </summary>
    /// <param name="rawLine">Raw text to be cleaned.</param>
    /// <returns>A cleaned/normalized version of the input text.</returns>
    string CleanLine(string rawLine);
}
