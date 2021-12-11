using System.Collections;

namespace AdventOfCode.Day09;

internal class Basin : IEnumerable<PointWithValue<int>>
{
    private readonly List<PointWithValue<int>> _list = new();
    
    public int Count => _list.Count;
    
    public void AddPoint(PointWithValue<int> point) => 
        _list.Add(point);
    
    public bool Contains(PointWithValue<int> point) => 
        _list.Contains(point);

    public IEnumerator<PointWithValue<int>> GetEnumerator() => 
        _list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();
}