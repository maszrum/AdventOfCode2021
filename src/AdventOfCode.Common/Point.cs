namespace AdventOfCode.Common;

public readonly record struct Point(int X, int Y)
{
    public static Point Zero => new (0, 0);
    
    public static Point Invalid => new(int.MaxValue, int.MinValue);
    
    public static bool IsInvalid(Point point) => 
        point.X == int.MaxValue && point.Y == int.MinValue;
    
    public static bool IsValid(Point point) => 
        !IsInvalid(point);
}