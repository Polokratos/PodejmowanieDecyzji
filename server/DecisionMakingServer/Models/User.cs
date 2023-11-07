namespace DecisionMakingServer.Models;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;

    public List<UserPoll> UserPolls { get; } = new();
    public List<UserAlternative> UserAlternatives { get; } = new();
}