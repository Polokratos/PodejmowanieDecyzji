using DecisionMakingServer.Calculation;
using DecisionMakingServer.Models;

namespace DecisionMakingServer.Tests;

public class CalculatorTests
{
    private readonly RankingCalculator _calculator;

    public CalculatorTests(Ranking ranking)
    {
        _calculator = new RankingCalculator(ranking);
    }
        
        
    public void Test()
    {
        _calculator.Calculate();
    }
}