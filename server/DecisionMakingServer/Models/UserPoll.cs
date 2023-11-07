using System.ComponentModel.DataAnnotations.Schema;

namespace DecisionMakingServer.Models;

public class UserPoll
{
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    [ForeignKey("Poll")]
    public int PollId { get; set; }
    public Poll Poll { get; set; } = null!;
    
    public bool IsFinished { get; set; } = false;
}