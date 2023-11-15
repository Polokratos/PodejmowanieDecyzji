namespace DecisionMakingServer.Models;

public class Ranking
{
    public int RankingId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = String.Empty;
    public CalculationMethod CalculationMethod { get; set; }
    public AggregationMethod AggregationMethod { get; set; }
    public bool IsComplete { get; set; }
    public string AskOrder = string.Empty;
    public DateTime CreationDate = DateTime.Now;
    public DateTime EndDate;

    public List<Alternative> Alternatives = new();
    public List<Criterion> Criteria = new();
    public List<UserRanking> UserRankings = new();
    public List<Scale> Scales = new();
    public List<Result> Results = new();
}