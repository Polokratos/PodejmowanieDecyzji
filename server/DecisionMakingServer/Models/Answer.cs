namespace DecisionMakingServer.Models;

public class Answer
{
    public int AnswerId { get; set; }
    
    public int RankingId { get; set; }
    public Ranking Ranking { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public int LeftAlternativeId { get; set; }
    public Alternative LeftAlternative { get; set; } = null!;
    
    public int RightAlternativeId { get; set; }
    public Alternative RightAlternative { get; set; } = null!;
    
    public float Value { get; set; }
}