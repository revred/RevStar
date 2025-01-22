namespace RevStar.Foundation;

/// <summary>
/// Holds hyperparameters and configuration details for constructing a model.
/// Example usage: pass this to a ModelBuilder or Trainer to create or load the correct model architecture.
/// </summary>
public class ModelConfig
{
    /// <summary>
    /// The size of each token embedding vector (dimensionality).
    /// </summary>
    public int EmbeddingSize { get; set; }

    /// <summary>
    /// Number of encoder/decoder layers in the Transformer or recurrent stack.
    /// </summary>
    public int NumLayers { get; set; }

    /// <summary>
    /// Number of attention heads per attention layer.
    /// </summary>
    public int NumHeads { get; set; }

    /// <summary>
    /// The maximum sequence length the model can handle.
    /// </summary>
    public int MaxSequenceLength { get; set; }

    /// <summary>
    /// Size of the feed-forward network inside each Transformer layer.
    /// Typically some multiple of EmbeddingSize.
    /// </summary>
    public int FeedForwardSize { get; set; }

    /// <summary>
    /// Vocabulary size (number of unique tokens).
    /// </summary>
    public int VocabSize { get; set; }

    /// <summary>
    /// (Optional) The path or file name where model weights should be saved/loaded from.
    /// </summary>
    public string? ModelCheckpointPath { get; set; }

    // Additional hyperparameters (e.g., dropout rate, learning rate) 
    // can be stored here or in a separate TrainingConfig.
}


/// <summary>
/// Holds hyperparameters specific to the training process
/// (as opposed to the structural details of the model).
/// </summary>
public class TrainingConfig
{
    public int BatchSize { get; set; }
    public int NumEpochs { get; set; }
    public float LearningRate { get; set; }
    public float WeightDecay { get; set; }
    public bool Shuffle { get; set; }
    // Add more training parameters as needed
}