using DecisionMakingServer.APIModels;

namespace DecisionMakingServer.Models;

public class Criterion
{
    public int CriterionId { get; set; }
    
    public int RankingId { get; set; }
    public Ranking Ranking { get; set; } = null!;
    
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public int? ParentId { get; set; }
    public Criterion? Parent { get; set; } = null;

    public List<CriterionAnswer> CriterionAnswers = new();
    public List<Answer> Answers = new();
    public List<Result> Results = new();
}


public static class CriteriaExtensions
{
    public static CriterionDTO ToDto(this Criterion c)
    {
        return new CriterionDTO
        {
            CriterionId = c.CriterionId,
            Name = c.Name,
            Description = c.Description,
        };
    }
}