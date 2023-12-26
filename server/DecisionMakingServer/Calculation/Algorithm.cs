using DecisionMakingServer.Extensions;
using DecisionMakingServer.Models;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace DecisionMakingServer.Calculation;

public static class Algorithm
{
    private static void AssertSquare(Matrix<double> m)
    {
        if (m.RowCount != m.ColumnCount)
            throw new ArgumentException("Matrix is not square.");
    }
    
    public static Vector<double> CalculateGmm(Matrix<double> matrix)
    {
        AssertSquare(matrix);
        
        var gms = matrix
            .FoldByRow((acc, next) => acc * next, 1.0)
            .Select(p => Math.Pow(p, 1.0 / matrix.RowCount))
            .ToList();

        double sum = gms.Sum();
        var result = Vector.Build.DenseOfEnumerable(gms.Select(x => x / sum));
        return result;
    }

    public static Vector<double> CalculateEvm(Matrix<double> matrix)
    {
        AssertSquare(matrix);
        
        var evd = matrix.Evd();
        int maxEv = evd.EigenValues
            .Where(x => x.Imaginary <= double.Epsilon)
            .Select(x => x.Real)
            .ArgMax();
        
        var ev = evd.EigenVectors.Column(maxEv);
        return ev.Divide(ev.Sum());
    }

    public static Vector<double> GetPriorityVector(this Matrix<double> matrix, CalculationMethod calculationMethod)
    {
        return calculationMethod switch
        {
            CalculationMethod.EVM => CalculateEvm(matrix),
            CalculationMethod.GMM => CalculateGmm(matrix),
            _ => throw new NotImplementedException("Calculation method is not implemented")
        };
    }

    public static Vector<double> MergeLayers(Vector<double> criteriaPrio, Vector<double>[] alternativesPrio)
    {
        var mergedAlts = Matrix.Build.DenseOfColumnVectors(alternativesPrio);
        return mergedAlts * criteriaPrio;
    }


    public static IEnumerable<double> MapResults(Vector<double> v)
    {
        var func = new Func<double, double>(x => x / (x + 1));
        var mapped = v.Select(x => func(x));
        double max = mapped.Max();
        var final = mapped.Select(x => x * 10 / max);
        return final;
    }
}