namespace RevStar.Foundation;

/// <summary>
/// Encapsulates a batch of data to be used in training or inference.
/// Typically contains raw text or token IDs, plus optional labels or metadata.
/// </summary>
public class DataBatch
{
    /// <summary>
    /// A collection of sequences (e.g., raw lines or tokenized IDs).
    /// In a simple scenario, these might be raw lines of text.
    /// </summary>
    public IReadOnlyList<string> Lines { get; }

    /// <summary>
    /// Optional numeric labels, if this batch is for supervised tasks (e.g., classification).
    /// For purely generative LLM tasks, this may be null.
    /// </summary>
    public IReadOnlyList<int>? Labels { get; }

    /// <summary>
    /// The size of this batch (e.g., number of lines).
    /// </summary>
    public int Count => Lines.Count;

    /// <summary>
    /// Constructs a new DataBatch, typically provided by a loader or after tokenization.
    /// </summary>
    /// <param name="lines">A list of text lines or sequences.</param>
    /// <param name="labels">Optional labels for each line, can be null if not needed.</param>
    public DataBatch(IReadOnlyList<string> lines, IReadOnlyList<int>? labels = null)
    {
        Lines = lines;
        Labels = labels;
    }
}
