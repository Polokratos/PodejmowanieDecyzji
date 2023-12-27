using DecisionMakingServer.Extensions;
using DecisionMakingServer.Models;
using MathNet.Numerics.Statistics;

namespace DecisionMakingServer.Calculation;

public class JudgementMeanRankingCalculator : RankingCalculator
{
    private readonly RankingMatrices _matrices;
    public JudgementMeanRankingCalculator(Ranking ranking) : base(ranking)
    {
        var contexts = InitializeContexts();
        _matrices = BuildMatrices(contexts);
    }

    private List<AnswerContext> InitializeContexts()
    {
        var aggregatedData = new Dictionary<AnswerContext, List<double>>();
        
        foreach (var answer in RankingData.Answers)
        {
            var context = new AnswerContext
            {
                CriterionId = answer.CriterionId,
                LeftId = answer.LeftAlternativeId,
                RightId = answer.RightAlternativeId
            };
            
            if (!aggregatedData.ContainsKey(context))
                aggregatedData.Add(context, new List<double>());
            
            aggregatedData[context].Add(answer.Value);
        }

        foreach (var answer in RankingData.CriterionAnswers)
        {
            var context = new AnswerContext
            {
                CriterionId = null,
                LeftId = answer.LeftCriterionId,
                RightId = answer.RightCriterionId
            };
            
            if (!aggregatedData.ContainsKey(context))
                aggregatedData.Add(context, new List<double>());
            
            aggregatedData[context].Add(answer.Value);
        }

        foreach (var kv in aggregatedData)
        {
            kv.Key.Value = kv.Value.GeometricMean();
        }
        return aggregatedData.Keys.ToList();
    }
    
    private RankingMatrices BuildMatrices(List<AnswerContext> answerContexts)
    {
        var rm = new RankingMatrices(NCriteria, NAlternatives);

        foreach (var answer in answerContexts)
        {
            double v = answer.Value ?? -1;
            
            if (answer.CriterionId == null)
            {
                int l = CriteriaToMatrix[answer.LeftId];
                int r = CriteriaToMatrix[answer.RightId];
                rm.CriteriaMatrix[l, r] = v;
                rm.CriteriaMatrix[r, l] = 1 / v;
            }
            else
            {
                int l = ToMatrix[answer.LeftId];
                int r = ToMatrix[answer.RightId];
                int c = CriteriaToMatrix[answer.CriterionId ?? -1];
                rm.AltMatrices[c][l, r] = v;
                rm.AltMatrices[c][r, l] = 1 / v;
            }
        }

        return rm;
    }
    
    public override IEnumerable<Result> Calculate()
    {
        // Calculate Priorities
        var criteriaPriority = _matrices.CriteriaMatrix.GetPriorityVector(CalculationMethod);
        var alternativesPriority = _matrices.AltMatrices.Select(m => m.GetPriorityVector(CalculationMethod)).ToArray();
        
        // Merge subrankings
        var result = Algorithm.MergeLayers(criteriaPriority, alternativesPriority);
        Console.WriteLine(result);

        // Map numbers and convert to DTO
        var mapped = Algorithm.MapResults(result);
        return mapped
            .Enumerate()
            .Select(kv => new Result 
            {
                RankingId = RankingData.RankingId,
                AlternativeId = ToDbId[kv.Item1],
                Score = kv.Item2
            }).ToList();
    }
}