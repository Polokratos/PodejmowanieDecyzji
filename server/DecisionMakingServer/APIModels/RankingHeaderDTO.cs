using Newtonsoft.Json;

namespace DecisionMakingServer.APIModels;

public class RankingHeaderDTO
{
    [JsonProperty("rankingId")] public int Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("description")] public string Description { get; set; } = string.Empty;
}