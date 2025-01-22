using RevStar.Foundation; // For domain types, e.g., DataBatch

namespace RevStar.Loader;


/// <summary>
/// Primary interface for loading and batching data within the RevStar ecosystem.
/// Combines the responsibilities of reading from a data source, cleaning text, 
/// splitting into partitions, and serving batches.
/// </summary>
public interface IDataLoader
{
    /// <summary>
    /// Configures an IDataSource (file, directory, remote) from which raw data will be read.
    /// </summary>
    /// <param name="dataSource">The data source to read from.</param>
    void SetDataSource(IDataSource dataSource);

    /// <summary>
    /// Configures an IDataCleaner for text normalization or transformation.
    /// </summary>
    /// <param name="cleaner">Cleaner/transformer to apply per line.</param>
    void SetDataCleaner(IDataCleaner cleaner);

    /// <summary>
    /// Configures an IDataSplitter for dividing data into training/validation/test sets.
    /// Optional if the user is managing partitions themselves.
    /// </summary>
    /// <param name="splitter">The splitter implementation.</param>
    void SetDataSplitter(IDataSplitter splitter);

    /// <summary>
    /// Loads and processes all available data from the configured data source.
    /// Applies cleaning, optional splitting, and caches results for batching.
    /// </summary>
    /// <param name="trainRatio">Fraction of data for training (0.0 - 1.0).</param>
    /// <param name="valRatio">Fraction of data for validation (0.0 - 1.0).</param>
    /// <param name="testRatio">Fraction of data for testing (0.0 - 1.0).</param>
    void LoadData(double trainRatio = 0.8, double valRatio = 0.1, double testRatio = 0.1);

    /// <summary>
    /// Returns the next batch of data (lines) from the specified partition (train/val/test). 
    /// If end of partition is reached, returns null or an empty batch.
    /// </summary>
    /// <param name="partition">Which subset of the data to read from: "train", "val", or "test".</param>
    /// <param name="batchSize">Max number of lines to return in a single batch.</param>
    /// <returns>A DataBatch or a similar structure containing lines of text.</returns>
    DataBatch? GetNextBatch(string partition, int batchSize);

    /// <summary>
    /// Resets the internal cursor for the specified partition so that iteration restarts at the beginning.
    /// </summary>
    /// <param name="partition">One of "train", "val", or "test".</param>
    void ResetPartitionCursor(string partition);

    /// <summary>
    /// Total number of lines loaded for each partition (train, val, test).
    /// </summary>
    int TrainSize { get; }
    int ValidationSize { get; }
    int TestSize { get; }
}
