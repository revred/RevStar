namespace RevStar.Loader;

/// <summary>
/// An abstraction for reading raw lines (or documents) of data from a source.
/// Could be a file, directory, or remote API.
/// </summary>
public interface IDataSource
{
    /// <summary>
    /// Opens or initializes the data source so that it can begin reading.
    /// </summary>
    /// <param name="pathOrIdentifier">A path, URL, or unique identifier describing the location of the data.</param>
    void Open(string pathOrIdentifier);

    /// <summary>
    /// Reads the next chunk of data (e.g., next line or block of text). 
    /// Returns null if no more data is available.
    /// </summary>
    /// <returns>A line or document of raw text, or null if end of data is reached.</returns>
    string? ReadNext();

    /// <summary>
    /// Resets the data source back to the start, allowing a new iteration over all data.
    /// </summary>
    void Reset();

    /// <summary>
    /// Closes any open streams or resources used by this data source.
    /// </summary>
    void Close();
}
