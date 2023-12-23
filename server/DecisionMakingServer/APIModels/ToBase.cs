using DecisionMakingServer.Models;

namespace DecisionMakingServer.APIModels;

public static class ToBase
{
    public static Criterion ToCriterion(this CriterionDTO dto)
    {
        return new Criterion
        {
            Name = dto.Name,
            Description = dto.Description,
        };
    }
    
    public static Alternative ToAlternative(this AlternativeDTO dto)
    {
        return new Alternative
        {
            Name = dto.Name,
            Description = dto.Description,
        };
    }
    
    public static ScaleValue ToScale(this ScaleValueDTO dto)
    {
        return new ScaleValue
        {
            Value = dto.Value,
            Description = dto.Description
        };
    }
    
    public static Ranking ToRanking(this RankingDTO dto)
    {
        return new Ranking
        {
            Name = dto.Name,
            Description = dto.Description,
            AggregationMethod = dto.AggregationMethod,
            IsComplete = dto.IsComplete,
            Alternatives = dto.Alternatives.Select(c => c.ToAlternative()).ToList(),
            AskOrder = dto.AskOrder,
            CalculationMethod = dto.CalculationMethod,
            CreationDate = DateTime.Now,
            EndDate = dto.EndDate,
            Scale = new Scale
            {
                ScaleValues = dto.Scale.Select(svd => svd.ToScale()).ToList()
            },
            Criteria = dto.Criteria.Select(c => c.ToCriterion()).ToList(),
            UserRankings = new List<UserRanking>(),
            Results = new List<Result>()
        };
    }

    public static Answer ToAnswer(this RankingAnswerDTO dto, int userId)
    {
        return new Answer
        {
            CriterionId = dto.CriterionId,
            LeftAlternativeId = dto.LeftAlternativeId,
            RightAlternativeId = dto.RightAlternativeId,
            Value = dto.Value,
            UserId = userId
        };
    }
}