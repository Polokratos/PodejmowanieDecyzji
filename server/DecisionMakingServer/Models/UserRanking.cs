namespace DecisionMakingServer.Models;

public class UserRanking
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public int RankingId { get; set; }
    public Ranking Ranking { get; set; } = null!;

    public UserRole UserRole { get; set; }
}