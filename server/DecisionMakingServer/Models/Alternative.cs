namespace DecisionMakingServer.Models;

public class Alternative
{
    public int AlternativeId { get; set; }
    public string Text { get; set; } = string.Empty;
    
    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;

    public List<UserAlternative> UserAlternatives { get; } = new();
}