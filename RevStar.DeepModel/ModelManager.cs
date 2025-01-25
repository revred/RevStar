using System.Text.Json;

namespace RevStar.DeepModel;

public class ModelManager
{
    private object? _model;
    private readonly string _modelDirectory;
    private bool _isSessionInitialized;
    private bool _useGPU = false;

    public ModelManager(string modelDirectory)
    {
        if (string.IsNullOrWhiteSpace(modelDirectory))
            throw new ArgumentException("Model directory cannot be null or empty.", nameof(modelDirectory));

        _modelDirectory = modelDirectory;

        if (!Directory.Exists(_modelDirectory))
            Directory.CreateDirectory(_modelDirectory);
    }

    /// <summary>
    /// Loads a pre-trained model from the specified path.
    /// </summary>
    /// <param name="modelPath">Full path to the model file.</param>
    /// <returns>True if the model is loaded successfully, otherwise false.</returns>
    public bool LoadModel(string modelPath)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(modelPath))
                throw new ArgumentException("Model path cannot be null or empty.", nameof(modelPath));

            if (!File.Exists(modelPath))
                throw new FileNotFoundException($"Model file '{modelPath}' not found.");

            string modelJson = File.ReadAllText(modelPath);
            _model = JsonSerializer.Deserialize<dynamic>(modelJson);

            Console.WriteLine($"Model loaded successfully from '{modelPath}'.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading model: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Initializes a session for running inferences.
    /// </summary>
    /// <param name="useGPU">Specifies whether to use GPU for inference.</param>
    public void InitializeSession(bool useGPU = false)
    {
        try
        {
            if (_model == null)
                throw new InvalidOperationException("No model loaded. Please load a model before initializing a session.");

            _useGPU = useGPU;
            _isSessionInitialized = true;

            Console.WriteLine("Session initialized successfully." + (useGPU ? " Using GPU." : " Using CPU."));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing session: {ex.Message}");
        }
    }

    /// <summary>
    /// Runs inference on the provided inputs.
    /// </summary>
    /// <param name="inputs">A list of input arrays for the model.</param>
    /// <returns>A list of output arrays from the model.</returns>
    public List<float[]> RunInference(List<float[]> inputs)
    {
        try
        {
            if (!_isSessionInitialized)
                throw new InvalidOperationException("Session is not initialized. Please initialize the session before running inference.");

            if (_model == null)
                throw new InvalidOperationException("No model loaded. Please load a model before running inference.");

            Console.WriteLine("Running inference...");

            // Simulated inference logic (replace with actual implementation).
            List<float[]> outputs = new List<float[]>();
            foreach (var input in inputs)
            {
                // Example: Mock processing of input data.
                float[] output = new float[input.Length];
                for (int i = 0; i < input.Length; i++)
                {
                    output[i] = input[i] * 2; // Placeholder logic.
                }
                outputs.Add(output);
            }

            Console.WriteLine("Inference completed successfully.");
            return outputs;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error running inference: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Saves the current model to disk.
    /// </summary>
    /// <param name="modelName">Name to save the model as.</param>
    /// <returns>True if the model is saved successfully, otherwise false.</returns>
    public bool SaveModel(string modelName)
    {
        try
        {
            if (_model == null)
                throw new InvalidOperationException("No model is loaded or trained to save.");

            string modelPath = Path.Combine(_modelDirectory, modelName + ".json");
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string modelJson = JsonSerializer.Serialize(_model, jsonOptions);
            File.WriteAllText(modelPath, modelJson);

            Console.WriteLine($"Model saved as '{modelPath}'.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving model: {ex.Message}");
            return false;
        }
    }
}
