// Token.cs
namespace RevStar.TokenSet;

// TokenSetConfig.cs
public class TokenSetConfig
{
    public int MaxTokenLength { get; set; } = 256;
    public bool TrackFrequency { get; set; } = true;
    public string DefaultStoragePath { get; set; } = "./tokens.json";

    public void LoadConfig(string filePath)
    {
        if (!File.Exists(filePath)) return;
        var json = File.ReadAllText(filePath);
        var config = System.Text.Json.JsonSerializer.Deserialize<TokenSetConfig>(json);
        if (config != null)
        {
            MaxTokenLength = config.MaxTokenLength;
            TrackFrequency = config.TrackFrequency;
            DefaultStoragePath = config.DefaultStoragePath;
        }
    }

    public void SaveConfig(string filePath)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(this);
        File.WriteAllText(filePath, json);
    }
}
