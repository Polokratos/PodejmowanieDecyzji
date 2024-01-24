using DecisionMakingServer.APIModels;
using DecisionMakingServer.Calculation;
using DecisionMakingServer.Controllers;
using DecisionMakingServer.Enums;
using DecisionMakingServer.Models;
using DecisionMakingServer.Repositories;

namespace DecisionMakingServer.Tests;

public static class CalculatorTests
{
    public static void Run()
    {
        // var repo = new RankingRepository();
        // var ranking = repo.GetRankingWithAnswers(23) ?? throw new ArgumentNullException("repo.GetRankingWithAnswers(18)");
        
        // RankingCalculator jmCalculator = new JudgementMeanRankingCalculator(ranking);
        // RankingCalculator pmCalculator = new PriorityMeanRankingCalculator(ranking);
        //
        // jmCalculator.Calculate();
        // pmCalculator.Calculate();
        
        RequestManager.CalculateResults(23);
    }
}