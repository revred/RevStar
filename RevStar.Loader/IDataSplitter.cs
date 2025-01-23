
namespace RevStar.Loader;

public class IDataSplitter
{
    internal (IEnumerable<string> trainData, IEnumerable<string> valData, IEnumerable<string> testData) SplitData(List<string> allLines, double trainRatio, double valRatio, double testRatio)
    {
        throw new NotImplementedException();
    }
}
