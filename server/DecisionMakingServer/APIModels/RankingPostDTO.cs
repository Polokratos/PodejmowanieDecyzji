using Newtonsoft.Json;

namespace DecisionMakingServer.APIModels;

public class RankingPostDTO
{
    [JsonProperty("rankingId")] public int RankingId { get; set; }
    [JsonProperty("answers")] public IEnumerable<RankingAnswerDTO> Answers { get; set; }
}