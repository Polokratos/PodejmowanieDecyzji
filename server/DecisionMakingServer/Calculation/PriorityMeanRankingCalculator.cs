using DecisionMakingServer.Extensions;
using DecisionMakingServer.Models;

namespace DecisionMakingServer.Calculation;

public class PriorityMeanRankingCalculator : RankingCalculator
{
    private readonly Dictionary<int, RankingMatrices> _userMatrices;
    
    public PriorityMeanRankingCalculator(Ranking ranking) : base(ranking)
    {
        _userMatrices = BuildMatrices();
    }


    private Dictionary<int, RankingMatrices> BuildMatrices()
    {
        var countByUser = from answer in RankingData.Answers
            group answer by answer.UserId
            into g
            select new { user = g.Key, count = g.Count() };

        var goodUsers = new List<int>();
        foreach (var uc in countByUser)
        {
            int targetCount = NAlternatives * (NAlternatives - 1) / 2;
            if (uc.count >= targetCount)
            {
                goodUsers.Add(uc.user);
                continue;
            }
            Console.WriteLine($"[WARN] Ranking has not been completed by user {uc.user} with count {uc.count}" +
                              $"of {targetCount}");
        }

        var userMatrices = new Dictionary<int, RankingMatrices>();
        foreach (int userId in goodUsers)
        {
            userMatrices[userId] = new RankingMatrices(NCriteria, NAlternatives);
        }
        
        // Criteria matrix
        foreach (var ca in RankingData.CriterionAnswers)
        {
            if (!goodUsers.Contains(ca.UserId))
                continue;
            
            int l = CriteriaToMatrix[ca.LeftCriterionId];
            int r = CriteriaToMatrix[ca.RightCriterionId];
            userMatrices[ca.UserId].CriteriaMatrix[l, r] = ca.Value;
            userMatrices[ca.UserId].CriteriaMatrix[r, l] = 1 / ca.Value;
        }
    
        // Alternatives matrices
        foreach (var criterion in RankingData.Criteria)
        {
            int m = CriteriaToMatrix[criterion.CriterionId];
            foreach (var answer in RankingData.Answers.Where(a => a.CriterionId == criterion.CriterionId))
            {
                int l = ToMatrix[answer.LeftAlternativeId];
                int r = ToMatrix[answer.RightAlternativeId];
                userMatrices[answer.UserId].AltMatrices[m][l, r] = answer.Value;
                userMatrices[answer.UserId].AltMatrices[m][r, l] = 1 / answer.Value;
            }
        }

        return userMatrices;
    }

    public override IEnumerable<Result> Calculate()
    {
        var criteriaPriority = _userMatrices
             .Select(um => um.Value.CriteriaMatrix.GetPriorityVector(CalculationMethod))
             .Aggregate((v1, v2) => v1 + v2) / _userMatrices.Count;

        var alternativesPriorities = _userMatrices
            .Select(um => um.Value.AltMatrices.Select(am => am.GetPriorityVector(CalculationMethod)))
            .Aggregate((pvarr1, pvarr2) => 
                pvarr1.Zip(pvarr2).Select(vectors => vectors.First + vectors.Second)
            )
            .Select(v => v / _userMatrices.Count)
            .ToArray();
        
        
        var result = Algorithm.MergeLayers(criteriaPriority, alternativesPriorities);
        Console.WriteLine(result);
        
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