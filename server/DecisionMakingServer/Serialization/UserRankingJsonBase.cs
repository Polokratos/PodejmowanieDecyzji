using DecisionMakingServer.Models;

namespace DecisionMakingServer.Serialization;

public class UserRankingJsonBase
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}