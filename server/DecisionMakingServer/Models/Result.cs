using DecisionMakingServer.APIModels;

namespace DecisionMakingServer.Models;

public class Result
{
    public int ResultId { get; set; }
    
    public int RankingId { get; set; }
    public Ranking Ranking { get; set; } = null!;
    
    public int AlternativeId { get; set; }
    public Alternative Alternative { get; set; } = null!;
    public double Score;
}


public static class ResultExtensions
{
    public static ResultDTO ToDto(this Result r)
    {
        return new ResultDTO
        {
            RankingId = r.RankingId,
            AlternativeId = r.AlternativeId,
            Score = r.Score
        };
    }
}