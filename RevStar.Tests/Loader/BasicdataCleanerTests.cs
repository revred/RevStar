using RevStar.Loader;
using Xunit;

namespace RevStar.Tests.Loader;

public class BasicDataCleanerTests
{
    [Theory]
    [InlineData(" Hello world! ", "hello world")]
    [InlineData("SOME PUNCT!!!", "some punct")]
    [InlineData("NoChange", "nochange")]
    public void CleansLine_TrimLowerRemovePunct(string input, string expected)
    {
        var cleaner = new BasicDataCleaner(convertToLower: true);
        var result = cleaner.CleanLine(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ReturnsEmptyForNullOrEmptyLines()
    {
        var cleaner = new BasicDataCleaner();
        Assert.Equal(string.Empty, cleaner.CleanLine("   "));
        Assert.Equal(string.Empty, cleaner.CleanLine(""));
    }

    [Theory]
    [InlineData("SOME TEXT!!", false, "SOME TEXT")]
    [InlineData("SOME TEXT!!", true, "some text")]
    public void RespectsLowercaseSetting(string input, bool lowercase, string expected)
    {
        var cleaner = new BasicDataCleaner(lowercase);
        Assert.Equal(expected, cleaner.CleanLine(input));
    }
}
