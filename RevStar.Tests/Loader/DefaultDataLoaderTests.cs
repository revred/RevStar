using Xunit;
using RevStar.Foundation; // For DataBatch
using RevStar.Loader;

namespace RevStar.Tests.Loader;

public class DefaultDataLoaderTests
{
    [Fact]
    public void LoadDataShouldPopulateTrainValTest()
    {
        // Arrange
        var filePath = Path.GetTempFileName();
        File.WriteAllLines(filePath, new[]
        {
            "Alpha!", "Beta??", "   Gamma   ", "Delta.", "Epsilon!"
        });

        var dataSource = new FileDataSource();
        var cleaner = new BasicDataCleaner();
        var splitter = new SimpleDataSplitter(seed: 2023) { ShuffleBeforeSplit = false };

        var loader = new DefaultDataLoader();
        loader.SetDataSource(dataSource);
        loader.SetDataCleaner(cleaner);
        loader.SetDataSplitter(splitter);

        try
        {
            // Act
            // 60% train, 20% val, 20% test
            loader.LoadData(trainRatio: 0.6, valRatio: 0.2, testRatio: 0.2);

            // Assert
            Assert.Equal(3, loader.TrainSize);   // 3 lines in train
            Assert.Equal(1, loader.ValidationSize);
            Assert.Equal(1, loader.TestSize);

            // Check the first training batch of size 2
            DataBatch? batch1 = loader.GetNextBatch("train", 2);
            Assert.NotNull(batch1);
            Assert.Equal(2, batch1!.Count);

            // Another training batch
            DataBatch? batch2 = loader.GetNextBatch("train", 2); // Request 2, but only 1 remains
            Assert.NotNull(batch2);
            Assert.Equal(1, batch2.Count);

            // No more training data
            var batch3 = loader.GetNextBatch("train", 2);
            Assert.Null(batch3);
        }
        finally
        {
            File.Delete(filePath);
        }
    }

    [Fact]
    public void ThrowsIfDependenciesNotSet()
    {
        var loader = new DefaultDataLoader();
        // Not setting data source, cleaner, or splitter

        Assert.Throws<InvalidOperationException>(() => loader.LoadData());
    }

    [Fact]
    public void ResetPartitionCursorWorks()
    {
        // Setup with 3 lines in training
        var filePath = Path.GetTempFileName();
        File.WriteAllLines(filePath, new[] { "L1", "L2", "L3" });

        var dataSource = new FileDataSource();
        var cleaner = new BasicDataCleaner(false);
        var splitter = new SimpleDataSplitter(seed: 1) { ShuffleBeforeSplit = false };

        var loader = new DefaultDataLoader();
        loader.SetDataSource(dataSource);
        loader.SetDataCleaner(cleaner);
        loader.SetDataSplitter(splitter);

        try
        {
            loader.LoadData(1.0, 0.0, 0.0); // all into train
            Assert.Equal(3, loader.TrainSize);

            // Read 2 lines
            var batch = loader.GetNextBatch("train", 2);
            Assert.NotNull(batch);
            Assert.Equal(2, batch!.Count);

            // Reset cursor
            loader.ResetPartitionCursor("train");

            // Expect to see all lines again
            var batchAgain = loader.GetNextBatch("train", 2);
            Assert.NotNull(batchAgain);
            Assert.Equal(2, batchAgain!.Count);
        }
        finally
        {
            File.Delete(filePath);
        }
    }
}
