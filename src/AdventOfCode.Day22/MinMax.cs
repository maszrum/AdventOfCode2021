namespace AdventOfCode.Day22;

internal readonly record struct MinMax(long Min, long Max)
{
    public static MinMax Create(long a, long b) =>
        new(
            Math.Min(a, b),
            Math.Max(a, b));
}