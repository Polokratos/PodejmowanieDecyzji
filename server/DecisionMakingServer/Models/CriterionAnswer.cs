namespace DecisionMakingServer.Models;

public class CriterionAnswer
{
    public int CriterionAnswerId { get; set; }
    
    public int RankingId { get; set; }
    public Ranking Ranking { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public int LeftCriterionId { get; set; }
    public Criterion LeftCriterion { get; set; } = null!;
    
    public int RightCriterionId { get; set; }
    public Criterion RightCriterion { get; set; } = null!;
    
    public float Value { get; set; }
}