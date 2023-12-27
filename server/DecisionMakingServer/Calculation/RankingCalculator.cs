using DecisionMakingServer.Extensions;
using DecisionMakingServer.Models;
using MathNet.Numerics.LinearAlgebra;
using Matrix = MathNet.Numerics.LinearAlgebra.Double.Matrix;

namespace DecisionMakingServer.Calculation;

public abstract class RankingCalculator
{
    protected readonly Ranking RankingData;
    protected readonly int NCriteria, NAlternatives;
    protected readonly CalculationMethod CalculationMethod;

    protected readonly Dictionary<int, int> ToMatrix = new();
    protected readonly Dictionary<int, int> ToDbId = new();
    protected readonly Dictionary<int, int> CriteriaToMatrix = new();
    private readonly Dictionary<int, int> _criteriatoDbId = new();
    
    
    protected class AnswerContext
    {
        public int? CriterionId { get; set; }
        public int LeftId { get; set; }
        public int RightId { get; set; }

        public double? Value { get; set; } = null;
    }
    protected class RankingMatrices
    {
        public Matrix<double> CriteriaMatrix { get; set; }
        public Matrix<double>[] AltMatrices { get; set; }

        public RankingMatrices(int nCriteria, int nAlternatives)
        {
            CriteriaMatrix = Matrix.Build.DenseIdentity(nCriteria, nCriteria);
            
            AltMatrices = new Matrix<double>[nCriteria];
            for (var i = 0; i < nCriteria; i++)
            {
                AltMatrices[i] = Matrix.Build.DenseIdentity(nAlternatives, nAlternatives);
            }
        }
    }
    
    
    protected RankingCalculator(Ranking ranking)
    {
        RankingData = ranking;
        CalculationMethod = ranking.CalculationMethod;
        NCriteria = ranking.Criteria.Count;
        NAlternatives = ranking.Alternatives.Count;
        
        InitializeMaps(ranking);
    }


    private void InitializeMaps(Ranking ranking)
    {
        foreach ((int i, var alternative) in ranking.Alternatives.Enumerate())
        {
            ToMatrix.Add(alternative.AlternativeId, i);
            ToDbId.Add(i, alternative.AlternativeId);
        }

        foreach ((int i, var criterion) in ranking.Criteria.Enumerate())
        {
            CriteriaToMatrix.Add(criterion.CriterionId, i);
            _criteriatoDbId.Add(i, criterion.CriterionId);
        }
    }
    
    public new abstract IEnumerable<Result> Calculate();
}