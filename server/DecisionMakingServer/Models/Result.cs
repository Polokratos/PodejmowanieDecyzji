namespace DecisionMakingServer.Models;

public class Result
{
    public int ResultId { get; set; }
    
    public int RankingId { get; set; }
    public Ranking Ranking { get; set; } = null!;
    
    public int CriterionId { get; set; }
    public Criterion Criterion { get; set; } = null!;
    
    public int AlternativeId { get; set; }
    public Alternative Alternative { get; set; } = null!;

    public int Place;
    public float Score;
}