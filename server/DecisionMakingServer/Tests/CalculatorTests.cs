using DecisionMakingServer.Calculation;
using DecisionMakingServer.Models;

namespace DecisionMakingServer.Tests;

public class CalculatorTests
{
    private readonly RankingCalculator _jmCalculator, _pmCalculator;

    public CalculatorTests(Ranking ranking)
    {
        _jmCalculator = new JudgementMeanRankingCalculator(ranking);
        _pmCalculator = new PriorityMeanRankingCalculator(ranking);
    }
        
        
    public void Test()
    {
        _jmCalculator.Calculate();
        _pmCalculator.Calculate();
    }
}