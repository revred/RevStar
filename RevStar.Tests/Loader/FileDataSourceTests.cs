
using Xunit;
using RevStar.Loader;

namespace RevStar.Tests.Loader;

public class FileDataSourceTests
{
    [Fact]
    public void ShouldReadLinesFromSingleFile()
    {
        // Arrange
        var filePath = Path.GetTempFileName();
        File.WriteAllLines(filePath, new[] { "Line1", "Line2", "Line3" });

        var dataSource = new FileDataSource();

        try
        {
            // Act
            dataSource.Open(filePath);

            var line1 = dataSource.ReadNext();
            var line2 = dataSource.ReadNext();
            var line3 = dataSource.ReadNext();
            var line4 = dataSource.ReadNext(); // Expect null

            // Assert
            Assert.Equal("Line1", line1);
            Assert.Equal("Line2", line2);
            Assert.Equal("Line3", line3);
            Assert.Null(line4);
        }
        finally
        {
            dataSource.Close();
            File.Delete(filePath);
        }
    }

    [Fact]
    public void ResetShouldRestartReading()
    {
        var filePath = Path.GetTempFileName();
        File.WriteAllLines(filePath, new[] { "Alpha", "Beta" });

        var dataSource = new FileDataSource();

        try
        {
            dataSource.Open(filePath);
            Assert.Equal("Alpha", dataSource.ReadNext());
            Assert.Equal("Beta", dataSource.ReadNext());

            // Reset
            dataSource.Reset();
            // Reading again from start
            Assert.Equal("Alpha", dataSource.ReadNext());
        }
        finally
        {
            dataSource.Close();
            File.Delete(filePath);
        }
    }

    [Fact]
    public void ThrowsIfFileNotFound()
    {
        var dataSource = new FileDataSource();
        Assert.Throws<FileNotFoundException>(() => dataSource.Open("NonExistentFile.txt"));
    }
}
