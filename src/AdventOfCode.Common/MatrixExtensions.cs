namespace AdventOfCode.Common;

public static class MatrixExtensions
{
    public static void IncrementEvery(this MutableMatrix<int> matrix) => 
        matrix.Transform(value => ++value);
    
    public static void DecrementEvery(this MutableMatrix<int> matrix) => 
        matrix.Transform(value => --value);
    
    public static IEnumerable<Point> MatrixWhere<T>(
        this Matrix<T> matrix, Predicate<T> predicate)
    {
        for (var y = 0; y < matrix.Rows; y++)
        {
            for (var x = 0; x < matrix.Columns; x++)
            {
                var point = new Point(x, y);
                var value = matrix.GetValue(point);
                
                if (predicate(value))
                {
                    yield return point;
                }
            }
        }
    }
    
    public static IEnumerable<string> ToString<T>(this Matrix<T> matrix, Func<T, string> formatter) => 
        matrix.Select(line => string.Concat(line.Select(formatter)));
    
    public static IEnumerable<string> ToString<T>(this Matrix<T> matrix, Func<T, Point, string> formatter) => 
        matrix.Select((line, y) => string.Concat(line.Select((value, x) => formatter(value, new Point(x, y)))));
}