using DecisionMakingServer.Calculation;
using DecisionMakingServer.Models;
using DecisionMakingServer.Repositories;

namespace DecisionMakingServer.Tests;

public static class CalculatorTests
{
    public static void Run()
    {
        var repo = new RankingRepository();
        var ranking = repo.GetRankingWithAnswers(18) ?? throw new ArgumentNullException("repo.GetRankingWithAnswers(18)");
        
        RankingCalculator jmCalculator = new JudgementMeanRankingCalculator(ranking);
        RankingCalculator pmCalculator = new PriorityMeanRankingCalculator(ranking);
        
        jmCalculator.Calculate();
        pmCalculator.Calculate();
    }
}