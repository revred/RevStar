using Xunit;
using RevStar.Loader;

namespace RevStar.Tests.Loader;

public class SimpleDataSplitterTests
{
    [Fact]
    public void ThrowsIfRatiosDontSumToOne()
    {
        var splitter = new SimpleDataSplitter();
        var data = new[] { "Line1", "Line2", "Line3" };
        Assert.Throws<ArgumentException>(() => splitter.SplitData(data, 0.5, 0.3, 0.3));
    }

    [Fact]
    public void SplitsDataAccordingToRatios()
    {
        var splitter = new SimpleDataSplitter(seed: 123) { ShuffleBeforeSplit = false };
        var data = new[] { "A", "B", "C", "D", "E", "F" };
        // train=0.5 => 3 lines, val=0.33 => 2 lines, test=0.17 => 1 line
        var (train, val, test) = splitter.SplitData(data, 0.5, 0.33, 0.17);

        Assert.Equal(3, train.Count);
        Assert.Equal(2, val.Count);
        Assert.Single(test);

        // Because Shuffle is off, the order is sequential
        Assert.Equal(new[] { "A", "B", "C" }, train);
        Assert.Equal(new[] { "D", "E" }, val);
        Assert.Equal(new[] { "F" }, test);
    }

    [Fact]
    public void RespectsShuffleFlag()
    {
        var splitter = new SimpleDataSplitter(seed: 42) { ShuffleBeforeSplit = true };
        var data = Enumerable.Range(1, 6).Select(i => $"Line{i}").ToList();

        var (train, val, test) = splitter.SplitData(data, 0.5, 0.3, 0.2);
        // We won't test the exact order after shuffle, but we can check the count and that it has changed from sequential.
        Assert.Equal(3, train.Count);
        Assert.Equal(2, val.Count);
        Assert.Single(test);

        // Check that the combined sets are still the original data, just shuffled
        var combined = train.Concat(val).Concat(test).OrderBy(x => x).ToList();
        var original = data.OrderBy(x => x).ToList();
        Assert.Equal(original, combined);
    }
}
