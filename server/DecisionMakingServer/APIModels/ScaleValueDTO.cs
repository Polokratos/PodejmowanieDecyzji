using Newtonsoft.Json;

namespace DecisionMakingServer.APIModels;

public class ScaleValueDTO
{
    [JsonProperty("value")] public int Value { get; set; } // Maximum value of given interval
    [JsonProperty("description")] public string Description { get; set; } = string.Empty;
}