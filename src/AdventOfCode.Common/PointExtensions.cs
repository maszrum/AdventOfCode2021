namespace AdventOfCode.Common;

public static class PointExtensions
{
    private static readonly IReadOnlyList<Func<Point, Point>> NeighbourTransformations = 
        new Func<Point, Point>[]
        {
            p => p.ToDown(), // down
            p => p.ToRight(), // right
            p => p.ToLeft(), // left
            p => p.ToUp(), // up
            p => new Point(p.X - 1, p.Y - 1), // up-left
            p => new Point(p.X - 1, p.Y + 1), // up-right
            p => new Point(p.X + 1, p.Y - 1), // down-left
            p => new Point(p.X + 1, p.Y + 1) // down-right
        };

    public static PointWithValue<T> WithValue<T>(this Point point, T value) => 
        new(point.X, point.Y, value);
    
    public static Point WithoutValue<T>(this PointWithValue<T> pwv) => 
        new(pwv.X, pwv.Y);

    public static Point ToUp(this Point point, int shift = 1) => 
        point with { Y = point.Y + shift };
    
    public static Point ToDown(this Point point, int shift = 1) => 
        point with { Y = point.Y - shift };
    
    public static Point ToLeft(this Point point, int shift = 1) => 
        point with { X = point.X - shift };
    
    public static Point ToRight(this Point point, int shift = 1) => 
        point with { X = point.X + shift };
    
    public static IEnumerable<Point> GetNeighbours(
        this Point point, bool includeDiagonal = false, bool includeSelf = false)
    {
        var neighbours = NeighbourTransformations
            .Take(includeDiagonal ? 8 : 4)
            .Select(transformation => transformation(point));
        
        return includeSelf
            ? new[] { point }.Concat(neighbours)
            : neighbours;
    }
}