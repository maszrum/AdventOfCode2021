namespace AdventOfCode.Common;

public class MutableMatrix<T> : Matrix<T>
{
    public MutableMatrix(T[][] values) : base(values)
    {
    }
    
    public new T this[Point point]
    {
        get => GetValue(point);
        set => SetValue(point, value);
    }
    
    public void SetValue(Point point, T value) => 
        Values[point.Y][point.X] = value;
    
    public void Transform(Func<Point, T, T> transformation)
    {
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Columns; x++)
            {
                var point = new Point(x, y);
                var value = Values[y][x];
                Values[y][x] = transformation(point, value);
            }
        }
    }
    
    public void Transform(Func<T, T> transformation) => 
        Transform((_, value) => transformation(value));
}