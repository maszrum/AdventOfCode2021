using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Common;

public class Matrix<T> : IEnumerable<IReadOnlyList<T>>
{
    private readonly IReadOnlyList<Func<Point, Point>> _neighbourTransformations = 
        new Func<Point, Point>[]
        {
            p => new Point(p.X, p.Y - 1), // top
            p => new Point(p.X, p.Y + 1), // bottom
            p => new Point(p.X - 1, p.Y), // left
            p => new Point(p.X + 1, p.Y), // right
            p => new Point(p.X - 1, p.Y - 1), // top-left
            p => new Point(p.X - 1, p.Y + 1), // top-right
            p => new Point(p.X + 1, p.Y - 1), // bottom-left
            p => new Point(p.X + 1, p.Y + 1) // bottom-right
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
        TryGetValue(new Point(point.X, point.Y - 1), out value);

    public bool TryGetBottomValue(Point point, [NotNullWhen(true)] out T? value) => 
        TryGetValue(new Point(point.X, point.Y + 1), out value);

    public bool TryGetLeftValue(Point point, [NotNullWhen(true)] out T? value) => 
        TryGetValue(new Point(point.X - 1, point.Y), out value);

    public bool TryGetRightValue(Point point, [NotNullWhen(true)] out T? value) => 
        TryGetValue(new Point(point.X + 1, point.Y), out value);

    public IEnumerable<PointWithValue<T>> GetNeighbours(Point point, bool includeDiagonal = false)
    {
        return _neighbourTransformations
            .Select(transformation => transformation(point))
            .Take(includeDiagonal ? 8 : 4)
            .Select(
                transformedPoint => TryGetValue(transformedPoint, out var value)
                    ? new PointWithValue<T>(transformedPoint.X, transformedPoint.Y, value)
                    : new PointWithValue<T>(int.MaxValue, 0, value!))
            .Where(pwv => pwv.X != int.MaxValue);
    }

    public IEnumerator<IReadOnlyList<T>> GetEnumerator() => 
        Values
            .Cast<IReadOnlyList<T>>()
            .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();
}