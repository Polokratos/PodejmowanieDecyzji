using System.ComponentModel.DataAnnotations.Schema;

namespace DecisionMakingServer.Models;

public class Alternative
{
    public int AlternativeId { get; set; }
    
    public int RankingId { get; set; }
    public Ranking Ranking { get; set; } = null!;
    
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<Answer> LeftAnswers = new();
    public List<Answer> RightAnswers = new();
    public List<Result> Results = new();
}