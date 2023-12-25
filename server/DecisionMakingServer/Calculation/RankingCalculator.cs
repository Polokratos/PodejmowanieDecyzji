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
    private int _length;
    private CalculationMethod _calculationMethod;
    private Matrix<double> _matrix;

    private Dictionary<int, int> _toMatrixMap = new();
    private Dictionary<int, int> _toDbIdMap = new();
    
    
    public RankingCalculator(Ranking ranking)
    {
        _ranking = ranking;
        _calculationMethod = ranking.CalculationMethod;
        _length = ranking.Alternatives.Count;
        _matrix = Matrix.Build.Dense(_length, _length);
    }
    
    
    private void Extract(Ranking ranking)
    {
        foreach (var (i, alternative) in ranking.Alternatives.Enumerate())
        {
            _toMatrixMap.Add(alternative.AlternativeId, i);
            _toDbIdMap.Add(i, alternative.AlternativeId);
        }

        foreach (var answer in ranking.Answers)
        {
            int l = _toMatrixMap[answer.LeftAlternativeId];
            int r = _toMatrixMap[answer.RightAlternativeId];
            _matrix[l, r] = answer.Value;
            _matrix[r, l] = 1 / answer.Value;
        }

        for (var i = 0; i < _length; i++)
            _matrix[i, i] = 1;
        
        Console.WriteLine(_matrix);
    }


    private Vector<double> CalculateGmm()
    {
        var gms = _matrix
            .FoldByRow((acc, next) => acc * next, 1.0)
            .Select(p => Math.Pow(p, 1.0 / _length))
            .ToList();

        var sum = gms.Sum();
        var result = Vector.Build.DenseOfEnumerable(gms.Select(x => x / sum));
        return result;
    }

    private Vector<double> CalculateEvm()
    {
        var evd = _matrix.Evd();
        var maxEv = evd.EigenValues
            .Where(x => x.Imaginary <= double.Epsilon)
            .Select(x => x.Real)
            .ArgMax();
        var ev = evd.EigenVectors.Row(maxEv);
        return ev.Divide(ev.Sum());
    }   



    public List<Result> Calculate()
    {
        Extract(_ranking);
        var evm = CalculateEvm();
        var gmm = CalculateGmm();
        
        Console.WriteLine($"EVM: {evm}");
        Console.WriteLine($"GMM: {gmm}");

        return new List<Result>();
    }
}