namespace DecisionMakingServer.Models;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public byte[] Password { get; set; }
    
    public List<UserRanking> UserRankings { get; set; }
}