using System.ComponentModel.DataAnnotations.Schema;
using DecisionMakingServer.APIModels;

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

    public List<Answer> Answers = new();
    public List<Alternative> Alternatives = new();
    public List<Criterion> Criteria = new();
    public List<UserRanking> UserRankings = new();
    public List<Result> Results = new();
}


public static class RankingExtensions
{
    public static RankingDTO ToDto(this Ranking r)
    {
        return new RankingDTO
        {
            SessionToken = null,
            RankingId = r.RankingId,
            Name = r.Name,
            Description = r.Description,
            CalculationMethod = r.CalculationMethod,
            AggregationMethod = r.AggregationMethod,
            IsComplete = r.IsComplete,
            AskOrder = r.AskOrder,
            CreationDate = r.CreationDate,
            EndDate = r.EndDate,
            Scale = r.Scale?.ScaleValues.Select(sv => sv.ToDto()).ToList(),
            Alternatives = r.Alternatives.Select(a => a.ToDto()).ToList(),
            Criteria = r.Criteria.Select(c => c.ToDto()).ToList(),
            Results = r.Results.Select(r => r.ToDto()).ToList(),
        };
    }
}