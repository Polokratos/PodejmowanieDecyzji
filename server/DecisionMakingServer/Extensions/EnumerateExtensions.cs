namespace DecisionMakingServer.Extensions;

public static class EnumerateExtensions
{
    public static IEnumerable<(int, T)> Enumerate<T>(
        this IEnumerable<T> input,
        int start = 0
    )
    {
        int i = start;
        foreach (var t in input)
            yield return (i++, t);
    }

    public static int ArgMax<T>(this IEnumerable<T> sequence)
        where T : IComparable<T>
    {
        int maxIndex = -1;
        T maxValue = default(T);

        var index = 0;
        foreach (T value in sequence)
        {
            if (value.CompareTo(maxValue) > 0 || maxIndex == -1)
            {
                maxIndex = index;
                maxValue = value;
            }

            index++;
        }

        return maxIndex;
    }
}