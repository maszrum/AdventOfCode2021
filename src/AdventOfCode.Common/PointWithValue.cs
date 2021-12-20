namespace AdventOfCode.Common;

public readonly record struct PointWithValue<T>(int X, int Y, T Value)
{
    public static implicit operator Point(PointWithValue<T> pwv) => 
        new(pwv.X, pwv.Y);
    
    public static PointWithValue<T> Invalid() => 
        new(int.MaxValue, int.MinValue, default!);
}

public static class PointWithValue
{
    public static bool IsInvalid<T>(PointWithValue<T> point) => 
        point.X == int.MaxValue && point.Y == int.MinValue;
    
    public static bool IsValid<T>(PointWithValue<T> point) => 
        !IsInvalid(point);
}