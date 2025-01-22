using RevStar.Foundation; // For domain types, e.g., DataBatch

namespace RevStar.Loader;

/// <summary>
/// Ties together IDataSource, IDataCleaner, and IDataSplitter to read data,
/// clean it, optionally split it, and serve batches for train/validation/test.
/// </summary>
public class DefaultDataLoader : IDataLoader
{
    private IDataSource? _dataSource;
    private IDataCleaner? _dataCleaner;
    private IDataSplitter? _dataSplitter;

    private List<string> _trainLines = new();
    private List<string> _valLines = new();
    private List<string> _testLines = new();

    private int _trainIndex;
    private int _valIndex;
    private int _testIndex;

    public void SetDataSource(IDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public void SetDataCleaner(IDataCleaner cleaner)
    {
        _dataCleaner = cleaner;
    }

    public void SetDataSplitter(IDataSplitter splitter)
    {
        _dataSplitter = splitter;
    }

    public void LoadData(double trainRatio = 0.8, double valRatio = 0.1, double testRatio = 0.1)
    {
        if (_dataSource == null) throw new InvalidOperationException("DataSource is not set.");
        if (_dataCleaner == null) throw new InvalidOperationException("DataCleaner is not set.");
        if (_dataSplitter == null) throw new InvalidOperationException("DataSplitter is not set.");

        // 1) Open the source
        _dataSource.Open("");

        // 2) Read all lines, cleaning each
        var allLines = new List<string>();
        string? line;
        while ((line = _dataSource.ReadNext()) != null)
        {
            string cleaned = _dataCleaner.CleanLine(line);
            if (!string.IsNullOrWhiteSpace(cleaned))
            {
                allLines.Add(cleaned);
            }
        }
        _dataSource.Close();

        // 3) Split lines
        var (trainData, valData, testData) = _dataSplitter.SplitData(allLines, trainRatio, valRatio, testRatio);

        // 4) Store them internally
        _trainLines = new List<string>(trainData);
        _valLines = new List<string>(valData);
        _testLines = new List<string>(testData);

        // 5) Reset indexes
        _trainIndex = 0;
        _valIndex = 0;
        _testIndex = 0;
    }

    public DataBatch? GetNextBatch(string partition, int batchSize)
    {
        switch (partition.ToLowerInvariant())
        {
            case "train":
                return GetBatchFor(_trainLines, ref _trainIndex, batchSize);
            case "val":
            case "validation":
                return GetBatchFor(_valLines, ref _valIndex, batchSize);
            case "test":
                return GetBatchFor(_testLines, ref _testIndex, batchSize);
            default:
                throw new ArgumentException($"Unknown partition: {partition}");
        }
    }

    public void ResetPartitionCursor(string partition)
    {
        switch (partition.ToLowerInvariant())
        {
            case "train":
                _trainIndex = 0;
                break;
            case "val":
            case "validation":
                _valIndex = 0;
                break;
            case "test":
                _testIndex = 0;
                break;
            default:
                throw new ArgumentException($"Unknown partition: {partition}");
        }
    }

    public int TrainSize => _trainLines.Count;
    public int ValidationSize => _valLines.Count;
    public int TestSize => _testLines.Count;

    private DataBatch? GetBatchFor(List<string> lines, ref int currentIndex, int batchSize)
    {
        if (currentIndex >= lines.Count)
        {
            return null; // No more data
        }

        int remaining = lines.Count - currentIndex;
        int size = Math.Min(batchSize, remaining);

        var batchSlice = lines.GetRange(currentIndex, size);
        currentIndex += size;

        // Construct a DataBatch from the lines (no labels in this example)
        return new DataBatch(batchSlice);
    }
}
