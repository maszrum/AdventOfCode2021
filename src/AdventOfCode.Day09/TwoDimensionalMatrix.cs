using System.Collections;

namespace AdventOfCode.Day09;

internal class TwoDimensionalMatrix<T> : IEnumerable<IReadOnlyList<T>>
{
    private readonly IReadOnlyList<IReadOnlyList<T>> _values;

    public TwoDimensionalMatrix(IReadOnlyList<IReadOnlyList<T>> values)
    {
        if (values.Count == 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(values), "must contain at least one element");
        }
        
        _values = values;
    }
    
    public int Rows => _values.Count;
    
    public int Columns => _values[0].Count;
    
    public T GetValue(int x, int y) => _values[y][x];
    
    public bool TryGetTopValue(int x, int y, out T? value)
    {
        if (y == 0)
        {
            value = default;
            return false;
        }
        
        value = GetValue(x, y - 1);
        return true;
    }
    
    public bool TryGetBottomValue(int x, int y, out T? value)
    {
        if (y == Rows - 1)
        {
            value = default;
            return false;
        }
        
        value = GetValue(x, y + 1);
        return true;
    }
    
    public bool TryGetLeftValue(int x, int y, out T? value)
    {
        if (x == 0)
        {
            value = default;
            return false;
        }

        value = GetValue(x - 1, y);
        return true;
    }
    
    public bool TryGetRightValue(int x, int y, out T? value)
    {
        if (x == Columns - 1)
        {
            value = default;
            return false;
        }
        
        value = GetValue(x + 1, y);
        return true;
    }

    public IEnumerator<IReadOnlyList<T>> GetEnumerator() => 
        _values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();
}