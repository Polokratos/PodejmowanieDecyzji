using Newtonsoft.Json;

namespace DecisionMakingServer.APIModels;

public class CriterionDTO
{
    [JsonProperty("criterionId")] public int? CriterionId { get; set; }
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("description")] public string Description { get; set; }
}