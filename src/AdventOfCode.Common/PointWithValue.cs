namespace AdventOfCode.Common;

public record struct PointWithValue<T>(int X, int Y, T Value)
{
    public static implicit operator Point(PointWithValue<T> pwv) => 
        new(pwv.X, pwv.Y);
}