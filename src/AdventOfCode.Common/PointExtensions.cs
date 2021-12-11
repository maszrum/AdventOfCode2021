namespace AdventOfCode.Common;

public static class PointExtensions
{
    public static PointWithValue<T> WithValue<T>(this Point point, T value) => 
        new(point.X, point.Y, value);

    public static Point ToUp(this Point point, int shift = 1) => 
        point with { Y = point.Y + shift };
    
    public static Point ToDown(this Point point, int shift = 1) => 
        point with { Y = point.Y - shift };
    
    public static Point ToLeft(this Point point, int shift = 1) => 
        point with { X = point.X - shift };
    
    public static Point ToRight(this Point point, int shift = 1) => 
        point with { X = point.X + shift };
}