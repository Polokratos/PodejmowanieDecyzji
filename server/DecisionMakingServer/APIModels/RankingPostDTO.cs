using Newtonsoft.Json;

namespace DecisionMakingServer.APIModels;

public class RankingPostDTO
{
    [JsonProperty("sessionToken")] public string SessionToken { get; set; } = string.Empty;
    [JsonProperty("rankingId")] public int RankingId { get; set; }
    [JsonProperty("answers")] public IEnumerable<RankingAnswerDTO> Answers { get; set; }
}