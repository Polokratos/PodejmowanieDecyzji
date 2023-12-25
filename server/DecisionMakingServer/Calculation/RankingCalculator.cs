using System.Collections;
using DecisionMakingServer.Extensions;
using DecisionMakingServer.Models;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Single;
using MathNet.Numerics.Providers.LinearAlgebra;
using Matrix = MathNet.Numerics.LinearAlgebra.Double.Matrix;
using Vector = MathNet.Numerics.LinearAlgebra.Double.Vector;

namespace DecisionMakingServer.Calculation;

public class RankingCalculator
{
    private Ranking _ranking;
    private int _nCriteria, _nAlternatives;
    private CalculationMethod _calculationMethod;

    private readonly Dictionary<int, int> _toMatrix = new();
    private Dictionary<int, int> _toDbId = new();
    private readonly Dictionary<int, int> _criteriaToMatrix = new();
    private Dictionary<int, int> _criteriatoDbId = new();
    
    
    private readonly Matrix<double> _criteriaMatrix;
    private readonly Matrix<double>[] _alternativeMatrices;
    
    public RankingCalculator(Ranking ranking)
    {
        _ranking = ranking;
        _calculationMethod = ranking.CalculationMethod;
        _nCriteria = ranking.Criteria.Count;
        _nAlternatives = ranking.Alternatives.Count;
        
        _criteriaMatrix = Matrix.Build.Dense(_nCriteria, _nCriteria);
        _alternativeMatrices = new Matrix<double>[_nCriteria];
        for (var i = 0; i < _nCriteria; i++)
        {
            _alternativeMatrices[i] = Matrix.Build.Dense(_nAlternatives, _nAlternatives);
        }
        
        InitializeMaps(ranking);
        InitializeMatrices(ranking);
    }


    private void InitializeMaps(Ranking ranking)
    {
        foreach ((int i, var alternative) in ranking.Alternatives.Enumerate())
        {
            _toMatrix.Add(alternative.AlternativeId, i);
            _toDbId.Add(i, alternative.AlternativeId);
        }

        foreach ((int i, var criterion) in ranking.Criteria.Enumerate())
        {
            _criteriaToMatrix.Add(criterion.CriterionId, i);
            _criteriatoDbId.Add(i, criterion.CriterionId);
        }
    }

    private void InitializeMatrices(Ranking ranking)
    {
        // Criteria matrix
        foreach (var ca in ranking.CriterionAnswers)
        {
            int l = _criteriaToMatrix[ca.LeftCriterionId];
            int r = _criteriaToMatrix[ca.RightCriterionId];
            _criteriaMatrix[l, r] = ca.Value;
            _criteriaMatrix[r, l] = 1 / ca.Value;
        }
        for (var i = 0; i < _nCriteria; i++)
            _criteriaMatrix[i, i] = 1;
        
        
        // Alternatives matrices
        foreach (var criterion in ranking.Criteria)
        {
            int m = _criteriaToMatrix[criterion.CriterionId];
            foreach (var answer in ranking.Answers)
            {
                int l = _toMatrix[answer.LeftAlternativeId];
                int r = _toMatrix[answer.RightAlternativeId];
                _alternativeMatrices[m][l, r] = answer.Value;
                _alternativeMatrices[m][r, l] = 1 / answer.Value;
            }
            
            for (var i = 0; i < _nCriteria; i++)
                _alternativeMatrices[m][i, i] = 1;
        }
    }
    


    public List<Result> Calculate()
    {
        Console.WriteLine(_criteriaMatrix);
        foreach (var matrix in _alternativeMatrices)
        {
            Console.WriteLine(matrix);
        }
        return new List<Result>();
    }
}