namespace AdventOfCode.Common;

public static class MatrixExtensions
{
    public static void IncrementEvery(this MutableTwoDimensionalMatrix<int> matrix) => 
        matrix.Transform(value => ++value);
    
    public static void DecrementEvery(this MutableTwoDimensionalMatrix<int> matrix) => 
        matrix.Transform(value => --value);
    
    public static int Increment(this MutableTwoDimensionalMatrix<int> matrix, int x, int y)
    {
        var value = matrix.GetValue(x, y) + 1;
        matrix.SetValue(x, y, value);
        return value;
    }

    public static int Decrement(this MutableTwoDimensionalMatrix<int> matrix, int x, int y)
    {
        var value = matrix.GetValue(x, y) - 1;
        matrix.SetValue(x, y, value);
        return value;
    }
    
    public static IEnumerable<Point> MatrixWhere<T>(
        this TwoDimensionalMatrix<T> matrix, Predicate<T> predicate)
    {
        for (var y = 0; y < matrix.Rows; y++)
        {
            for (var x = 0; x < matrix.Columns; x++)
            {
                var value = matrix.GetValue(x, y);
                if (predicate(value))
                {
                    yield return new Point(x, y);
                }
            }
        }
    }
}