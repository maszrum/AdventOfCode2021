using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Common;

public class Matrix<T> : IEnumerable<IReadOnlyList<T>>
{
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
        return point
            .GetNeighbours(includeDiagonal)
            .Select(
                neighbour => TryGetValue(neighbour, out var value)
                    ? neighbour.WithValue(value)
                    : PointWithValue<T>.Invalid())
            .Where(PointWithValue.IsValid);
    }

    public IEnumerator<IReadOnlyList<T>> GetEnumerator() => 
        Values
            .Cast<IReadOnlyList<T>>()
            .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();
}