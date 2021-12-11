namespace AdventOfCode.Common;

public class MutableMatrix<T> : Matrix<T>
{
    public MutableMatrix(T[][] values) : base(values)
    {
    }
    
    public void SetValue(int x, int y, T value) => 
        Values[y][x] = value;
    
    public void Transform(Func<int, int, T, T> transformation)
    {
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Columns; x++)
            {
                Values[y][x] = transformation(x, y, Values[y][x]);
            }
        }
    }
    
    public void Transform(Func<T, T> transformation)
    {
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Columns; x++)
            {
                Values[y][x] = transformation(Values[y][x]);
            }
        }
    }
}