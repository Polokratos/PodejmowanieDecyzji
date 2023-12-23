using System.ComponentModel.DataAnnotations.Schema;
using DecisionMakingServer.APIModels;

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


public static class AlternativeExtensions
{
    public static AlternativeDTO ToDto(this Alternative a)
    {
        return new AlternativeDTO
        {
            AlternativeId = a.AlternativeId,
            Name = a.Name,
            Description = a.Description
        };
    }
}