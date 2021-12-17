namespace AdventOfCode.Common;

public readonly record struct Point(int X, int Y)
{
    public static Point Zero => new (0, 0);
    
    public static Point Invalid => new(int.MaxValue, int.MinValue);
    
    public bool IsInvalid() => 
        X == int.MaxValue && Y == int.MinValue;
}