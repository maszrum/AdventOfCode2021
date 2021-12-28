namespace AdventOfCode.Common;

public readonly struct Pair<T>
{
    public Pair(T a, T b)
    {
        A = a;
        B = b;
    }
    
    public T A { get; }
    
    public T B { get; }
}

public static class Pair
{
    public static Pair<T> Create<T>(T a, T b) => new(a, b);
}