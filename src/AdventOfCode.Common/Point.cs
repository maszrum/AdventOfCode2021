namespace AdventOfCode.Common;

public readonly record struct Point(int X, int Y)
{
    public bool IsInvalid() => 
        X == int.MaxValue && Y == int.MinValue;
    
    public static Point Invalid() => 
        new(int.MaxValue, int.MinValue);
}