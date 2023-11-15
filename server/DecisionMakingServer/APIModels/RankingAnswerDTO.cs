using Newtonsoft.Json;

namespace DecisionMakingServer.APIModels;

public class RankingAnswerDTO
{
    [JsonProperty("criterionId")] public int CriterionId { get; set; }
    [JsonProperty("leftAlternativeId")] public int LeftAlternativeId { get; set; }
    [JsonProperty("rightAlternativeId")] public int RightAlternativeId { get; set; }
    [JsonProperty("value")] public float Value { get; set; }
}