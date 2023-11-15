using System.ComponentModel.DataAnnotations.Schema;

namespace DecisionMakingServer.Models;

public class Ranking
{
    public int RankingId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = String.Empty;
    public CalculationMethod CalculationMethod { get; set; }
    public AggregationMethod AggregationMethod { get; set; }
    public bool IsComplete { get; set; }
    public string AskOrder { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; } = null;
    
    public int ScaleId { get; set; }
    public Scale? Scale { get; set; } = new();

    public List<Alternative> Alternatives = new();
    public List<Criterion> Criteria = new();
    public List<UserRanking> UserRankings = new();
    public List<Result> Results = new();
}