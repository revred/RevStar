namespace RevStar.Loader;

/// <summary>
/// Splits data into train/val/test sets according to provided ratios.
/// Optionally shuffles the data first.
/// </summary>
public class SimpleDataSplitter : IDataSplitter
    {
        /// <summary>
        /// If true, the data is shuffled prior to splitting.
        /// </summary>
        public bool ShuffleBeforeSplit { get; set; } = true;

        private readonly Random _rng;

        public SimpleDataSplitter(int seed = 42)
        {
            _rng = new Random(seed);
        }

        /// <summary>
        /// Splits the given rawData into (train, val, test) sets by ratio.
        /// If ShuffleBeforeSplit = true, rawData is first randomized.
        /// </summary>
        public (IReadOnlyList<string> TrainData,
                IReadOnlyList<string> ValData,
                IReadOnlyList<string> TestData)
            SplitData(IEnumerable<string> rawData, double trainRatio, double validationRatio, double testRatio)
        {
            if (Math.Abs(trainRatio + validationRatio + testRatio - 1.0) > 0.0001)
            {
                throw new ArgumentException("Ratios must sum to 1.0");
            }

            // Convert to list
            var dataList = rawData.ToList();
            if (ShuffleBeforeSplit)
            {
                Shuffle(dataList);
            }

            int total = dataList.Count;
            int trainCount = (int)(total * trainRatio);
            int valCount = (int)(total * validationRatio);
            int testCount = total - trainCount - valCount;

            var trainData = dataList.Take(trainCount).ToList();
            var valData = dataList.Skip(trainCount).Take(valCount).ToList();
            var testData = dataList.Skip(trainCount + valCount).Take(testCount).ToList();

            return (trainData, valData, testData);
        }

        private void Shuffle<T>(IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int swapIndex = _rng.Next(i, list.Count);
                (list[i], list[swapIndex]) = (list[swapIndex], list[i]);
            }
        }
    }
