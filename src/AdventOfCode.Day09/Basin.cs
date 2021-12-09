using System.Collections;

namespace AdventOfCode.Day09;

internal class Basin : IEnumerable<Point>
{
    private readonly List<Point> _list = new();
    
    public int Count => _list.Count;
    
    public void AddPoint(Point point) => _list.Add(point);
    
    public bool Contains(Point point) => _list.Contains(point);

    public IEnumerator<Point> GetEnumerator() => 
        _list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => 
        GetEnumerator();
}