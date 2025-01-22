namespace RevStar.Loader;

/// <summary>
/// Reads lines from one or more local text files. Implements IDataSource.
/// </summary>
public class FileDataSource : IDataSource
{
    private readonly List<string> _filePaths = new();
    private StreamReader? _currentReader;
    private int _currentFileIndex;

    /// <summary>
    /// Opens the data source, which may be a single file or multiple files in a directory.
    /// </summary>
    /// <param name="pathOrIdentifier">
    /// If it's a file, reads lines from that file.
    /// If it's a directory, reads lines from all .txt files inside it.
    /// </param>
    public void Open(string pathOrIdentifier)
    {
        _filePaths.Clear();
        _currentFileIndex = 0;
        _currentReader?.Dispose();
        _currentReader = null;

        if (Directory.Exists(pathOrIdentifier))
        {
            // Load all .txt files in a directory, for example
            string[] txtFiles = Directory.GetFiles(pathOrIdentifier, "*.txt");
            _filePaths.AddRange(txtFiles);
        }
        else if (File.Exists(pathOrIdentifier))
        {
            // Single file
            _filePaths.Add(pathOrIdentifier);
        }
        else
        {
            throw new FileNotFoundException($"File or directory not found: {pathOrIdentifier}");
        }

        if (_filePaths.Count == 0)
        {
            throw new FileNotFoundException("No valid text files found.");
        }

        // Open the first file
        _currentReader = new StreamReader(_filePaths[_currentFileIndex]);
    }

    /// <summary>
    /// Reads the next line from the current file. Moves to the next file if end-of-file is reached.
    /// Returns null if no more data is available.
    /// </summary>
    /// <returns>A line of raw text or null if done.</returns>
    public string? ReadNext()
    {
        if (_filePaths.Count == 0)
            return null;

        while (_currentReader != null)
        {
            var line = _currentReader.ReadLine();
            if (line != null)
            {
                return line;
            }
            else
            {
                // End of current file
                _currentReader.Dispose();
                _currentReader = null;
                _currentFileIndex++;

                if (_currentFileIndex < _filePaths.Count)
                {
                    _currentReader = new StreamReader(_filePaths[_currentFileIndex]);
                    continue;
                }
                else
                {
                    // No more files
                    return null;
                }
            }
        }

        // If we get here, we're out of data
        return null;
    }

    /// <summary>
    /// Resets reading to the first file again, discarding current progress.
    /// </summary>
    public void Reset()
    {
        Close();
        if (_filePaths.Count > 0)
        {
            _currentFileIndex = 0;
            _currentReader = new StreamReader(_filePaths[_currentFileIndex]);
        }
    }

    /// <summary>
    /// Closes all resources and clears the file list.
    /// </summary>
    public void Close()
    {
        _currentReader?.Dispose();
        _currentReader = null;
        _currentFileIndex = 0;
    }
}
