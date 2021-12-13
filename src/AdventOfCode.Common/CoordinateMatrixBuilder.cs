namespace AdventOfCode.Common;

public class CoordinateMatrixBuilder<T>
{
    private readonly List<PointWithValue<T>> _points = new();

    public CoordinateMatrixBuilder<T> AddPoint(PointWithValue<T> point)
    {
        _points.Add(point);
        return this;
    }
    
    public CoordinateMatrixBuilder<T> AddPoint(int x, int y, T value) => 
        AddPoint(new PointWithValue<T>(x, y, value));
    
    public CoordinateMatrixBuilder<T> AddPoints(params PointWithValue<T>[] points)
    {
        _points.AddRange(points);
        return this;
    }
    
    public CoordinateMatrixBuilder<T> AddPoints(IEnumerable<PointWithValue<T>> points)
    {
        _points.AddRange(points);
        return this;
    }

    public Matrix<T> Build() => new(BuildArray());
    
    public MutableMatrix<T> BuildMutable() => new(BuildArray());

    private T[][] BuildArray()
    {
        var columns = _points.Max(p => p.X) + 1;
        var rows = _points.Max(p => p.Y) + 1;
        
        var array = Enumerable
            .Repeat(0, rows)
            .Select(_ => new T[columns])
            .ToArray();
        
        foreach (var point in _points)
        {
            var (x, y, value) = point;
            
            array[y][x] = value;
        }
        
        return array;
    }
}