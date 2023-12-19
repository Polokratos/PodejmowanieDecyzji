using DecisionMakingServer.Models;
using Newtonsoft.Json;

namespace DecisionMakingServer.APIModels;

public class RankingDTO
{
    [JsonProperty("sessionToken")] public string SessionToken { get; set; } = String.Empty;
    [JsonProperty("rankingId")] public int RankingId { get; set; }
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;
    [JsonProperty("description")] public string Description { get; set; } = String.Empty;
    [JsonProperty("calculationMethod")] public CalculationMethod CalculationMethod { get; set; }
    [JsonProperty("aggregationMethod")] public AggregationMethod AggregationMethod { get; set; }
    [JsonProperty("isComplete")] public bool IsComplete { get; set; }
    [JsonProperty("askOrder")] public string AskOrder { get; set; } = string.Empty;
    [JsonProperty("creationDate")] public DateTime? CreationDate { get; set; } = DateTime.Now;
    [JsonProperty("endDate")] public DateTime EndDate { get; set; }
    
    [JsonProperty("scale")] public List<ScaleValueDTO> Scale { get; set; } = new();
    [JsonProperty("alternatives")] public List<AlternativeDTO> Alternatives { get; set; } = new();
    [JsonProperty("criteria")] public List<CriterionDTO> Criteria { get; set; } = new();
    [JsonProperty("results")] public List<ResultDTO>? Results { get; set; } = new();
}