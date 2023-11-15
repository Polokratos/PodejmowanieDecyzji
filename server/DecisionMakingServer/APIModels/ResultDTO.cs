using Newtonsoft.Json;

namespace DecisionMakingServer.APIModels;

public class ResultDTO
{
    [JsonProperty("rankingId")] public int RankingId { get; set; }
    [JsonProperty("criterionId")] public int CriterionId { get; set; }
    [JsonProperty("alternativeId")] public int AlternativeId { get; set; }
    [JsonProperty("score")] public float Score;
}