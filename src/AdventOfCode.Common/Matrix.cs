using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Common;

public class Matrix<T> : IEnumerable<IReadOnlyList<T>>
{
    private readonly IReadOnlyList<Func<int, int, (int, int)>> _neighbourTransformations = 
        new Func<int, int, (int, int)>[]
        {
            (x, y) => (x, y - 1),
            (x, y) => (x, y + 1),
            (x, y) => (x - 1, y),
            (x, y) => (x + 1, y)
        };

    private readonly IReadOnlyList<Func<int, int, (int, int)>> _diagonalNeighbourTransformations = 
        new Func<int, int, (int, int)>[]
        {
            (x, y) => (x - 1, y - 1),
            (x, y) => (x - 1, y + 1),
            (x, y) => (x + 1, y - 1),
            (x, y) => (x + 1, y + 1)
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
    
    public int Rows => Values.Length;
    
    public int Columns => Values[0].Length;
    
    public T GetValue(int x, int y) => Values[y][x];
    
    public bool Exists(int x, int y) => 
        y < 0 || y >= Rows - 1 || x < 0 || x >= Columns - 1;

    public bool TryGetValue(int x, int y, [NotNullWhen(true)] out T? value)
    {
        if (y < 0 || y >= Rows || x < 0 || x >= Columns)
        {
            value = default;
            return false;
        }
        
        value = GetValue(x, y)!;
        return true;
    }
    
    public bool TryGetTopValue(int x, int y, [NotNullWhen(true)] out T? value) => 
        TryGetValue(x, y - 1, out value);

    public bool TryGetBottomValue(int x, int y, [NotNullWhen(true)] out T? value) => 
        TryGetValue(x, y + 1, out value);

    public bool TryGetLeftValue(int x, int y, [NotNullWhen(true)] out T? value) => 
        TryGetValue(x - 1, y, out value);

    public bool TryGetRightValue(int x, int y, [NotNullWhen(true)] out T? value) => 
        TryGetValue(x + 1, y, out value);

    public IEnumerable<PointWithValue<T>> GetNeighbours(
        int x, int y, bool includeDiagonal = false)
    {
        foreach (var transformation in _neighbourTransformations)
        {
            var (tX, tY) = transformation(x, y);
            
            if (TryGetValue(tX, tY, out var value))
            {
                yield return new PointWithValue<T>(tX, tY, value);
            }
        }
        
        if (!includeDiagonal)
        {
            yield break;
        }
        
        foreach (var transformation in _diagonalNeighbourTransformations)
        {
            var (tX, tY) = transformation(x, y);
            
            if (TryGetValue(tX, tY, out var value))
            {
                yield return new PointWithValue<T>(tX, tY, value);
            }
        }
    }

    public IEnumerator<IReadOnlyList<T>> GetEnumerator() => 
        Values
            .Cast<IReadOnlyList<T>>()
            .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();
}