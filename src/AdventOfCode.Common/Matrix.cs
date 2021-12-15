using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Common;

public class Matrix<T> : IEnumerable<IReadOnlyList<T>>
{
    private readonly IReadOnlyList<Func<Point, Point>> _neighbourTransformations = 
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
    
    protected readonly T[][] Values;

    public Matrix(T[][] values)
    {
        if (values.Length == 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(values), "must contain at least one element");
        }
        
        Values = values;
    }
    
    public T this[Point point] => GetValue(point);

    public int Rows => Values.Length;
    
    public int Columns => Values[0].Length;
    
    public T GetValue(Point point) => Values[point.Y][point.X];
    
    public T GetValue(int x, int y) => Values[y][x];
    
    public bool Exists(Point point) => 
        !(point.Y < 0 || point.Y >= Rows || point.X < 0 || point.X >= Columns);

    public bool TryGetValue(Point point, [NotNullWhen(true)] out T? value)
    {
        if (!Exists(point))
        {
            value = default;
            return false;
        }
        
        value = GetValue(point)!;
        return true;
    }
    
    public bool TryGetTopValue(Point point, [NotNullWhen(true)] out T? value) => 
        TryGetValue(point.ToUp(), out value);

    public bool TryGetBottomValue(Point point, [NotNullWhen(true)] out T? value) => 
        TryGetValue(point.ToDown(), out value);

    public bool TryGetLeftValue(Point point, [NotNullWhen(true)] out T? value) => 
        TryGetValue(point.ToLeft(), out value);

    public bool TryGetRightValue(Point point, [NotNullWhen(true)] out T? value) => 
        TryGetValue(point.ToRight(), out value);

    public IEnumerable<PointWithValue<T>> GetNeighbours(Point point, bool includeDiagonal = false)
    {
        return _neighbourTransformations
            .Select(transformation => transformation(point))
            .Take(includeDiagonal ? 8 : 4)
            .Select(
                transformedPoint => TryGetValue(transformedPoint, out var value)
                    ? transformedPoint.WithValue(value)
                    : PointWithValue<T>.Invalid())
            .Where(pwv => !pwv.IsInvalid());
    }

    public IEnumerator<IReadOnlyList<T>> GetEnumerator() => 
        Values
            .Cast<IReadOnlyList<T>>()
            .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();
}