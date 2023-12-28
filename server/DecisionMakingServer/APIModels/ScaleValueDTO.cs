using Newtonsoft.Json;

namespace DecisionMakingServer.APIModels;

public class ScaleValueDTO
{
    [JsonProperty("value")] public int Value { get; set; }
    [JsonProperty("description")] public string Description { get; set; } = string.Empty;
}