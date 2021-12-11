namespace AdventOfCode.Common;

public readonly record struct PointWithValue<T>(int X, int Y, T Value)
{
    public static implicit operator Point(PointWithValue<T> pwv) => 
        new(pwv.X, pwv.Y);
    
    public bool IsInvalid() => 
        X == int.MaxValue && Y == int.MinValue;
    
    public static PointWithValue<T> Invalid() => 
        new(int.MaxValue, int.MinValue, default!);
}