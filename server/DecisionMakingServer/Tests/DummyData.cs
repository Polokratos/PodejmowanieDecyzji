using DecisionMakingServer.APIModels;
using DecisionMakingServer.Models;

namespace DecisionMakingServer.Tests;

public class DummyData
{
    public static readonly Ranking Ranking = new()
    {
        RankingId = 6969,
        Name = "A test ranking",
        Description = "lol",
        AggregationMethod = AggregationMethod.JudgementMean,
        Alternatives = new List<Alternative>
        {
            new() { AlternativeId = 0, Name = "Alt1", Description = "Alt1d" },
            new() { AlternativeId = 1, Name = "Alt2", Description = "Alt2d" },
            new() { AlternativeId = 5, Name = "Alt3", Description = "Alt3d" },
            new() { AlternativeId = 42, Name = "Alt4", Description = "Alt4d" },
            new() { AlternativeId = 69, Name = "Alt5", Description = "Alt5d" },
        },
        CreationDate = DateTime.Now.Subtract(TimeSpan.FromHours(2)),
        AskOrder = "123",
        CalculationMethod = CalculationMethod.GeometricMean,
        Criteria = new List<Criterion>
        {
            new() { CriterionId = 0, Name = "Crit1", Description = "Crit1d" },
            new() { CriterionId = 13, Name = "Crit2", Description = "Crit2d" }
        },
        EndDate = DateTime.Now.Add(TimeSpan.FromDays(10)),
        IsComplete = false,
        Scale = new Scale
        {
            ScaleId = 732,
            RankingId = 6969,
            ScaleValues = new List<ScaleValue>
            {
                new() { Description = "bad", Value = 3 },
                new() { Description = "good", Value = 6 }
            }
        },
    };
    
    public static readonly Ranking NoIdRanking = new()
    {
        Name = "A test ranking for AAA",
        Description = "lolxd",
        AggregationMethod = AggregationMethod.JudgementMean,
        Alternatives = new List<Alternative>
        {
            new() { Name = "Alt31", Description = "Alt1d" },
            new() { Name = "Alt32", Description = "Alt2d" },
            new() { Name = "Alt33", Description = "Alt3d" },
            new() { Name = "Alt34", Description = "Alt4d" },
            new() { Name = "Alt35", Description = "Alt5d" },
        },
        CreationDate = DateTime.Now.Subtract(TimeSpan.FromHours(2)),
        AskOrder = "123",
        CalculationMethod = CalculationMethod.GeometricMean,
        Criteria = new List<Criterion>
        {
            new() { Name = "Crit1", Description = "Crit1d" },
            new() { Name = "Crit2", Description = "Crit2d" }
        },
        EndDate = DateTime.Now.Add(TimeSpan.FromDays(10)),
        IsComplete = false,
        Scale = new Scale
        {
            ScaleValues = new List<ScaleValue>
            {
                new() { Description = "bad", Value = 3 },
                new() { Description = "good", Value = 6 }
            }
        },
    };

    public static RankingDTO RankingDto = Ranking.ToDto();

    public static RankingPostDTO PostDto(Ranking ranking)
    {
        return new RankingPostDTO
        {
            SessionToken = "abcd",
            RankingId = ranking.RankingId,
            Answers = new List<RankingAnswerDTO>
            {
                new()
                {
                    CriterionId = ranking.Criteria[0].CriterionId,
                    LeftAlternativeId = ranking.Alternatives[0].AlternativeId,
                    RightAlternativeId = ranking.Alternatives[1].AlternativeId,
                    Value = 3,
                },
                new()
                {
                    CriterionId = ranking.Criteria[0].CriterionId,
                    LeftAlternativeId = ranking.Alternatives[1].AlternativeId,
                    RightAlternativeId = ranking.Alternatives[2].AlternativeId,
                    Value = -2
                },
                new()
                {
                    CriterionId = ranking.Criteria[0].CriterionId,
                    LeftAlternativeId = ranking.Alternatives[2].AlternativeId,
                    RightAlternativeId = ranking.Alternatives[0].AlternativeId,
                    Value = 5
                }
            }
        };
    }
}