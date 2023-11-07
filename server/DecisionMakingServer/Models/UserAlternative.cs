namespace DecisionMakingServer.Models;

public class UserAlternative
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public int AlternativeId { get; set; }
    public Alternative Alternative { get; set; } = null!;

    public string Comment { get; set; } = string.Empty;
}