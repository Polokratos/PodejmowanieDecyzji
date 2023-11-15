using Newtonsoft.Json;

namespace DecisionMakingServer.APIModels;

public class AlternativeDTO
{
    [JsonProperty("alternativeId")] public int? AlternativeId { get; set; }
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("description")] public string Description { get; set; }
}