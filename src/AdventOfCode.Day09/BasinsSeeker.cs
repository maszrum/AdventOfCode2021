namespace AdventOfCode.Day09;

internal class BasinsSeeker
{
    private readonly TwoDimensionalMatrix<int> _matrix;
    
    public BasinsSeeker(TwoDimensionalMatrix<int> matrix)
    {
        _matrix = matrix;
    }

    public IEnumerable<Basin> Find(IEnumerable<Point> minima)
    {
        var isInBasin = new bool[_matrix.Rows,_matrix.Columns];
        
        return minima
            .Where(point => !isInBasin[point.Y,point.X])
            .Select(point =>
            {
                var basin = FindBasinForPoint(point);

                foreach (var (x, y, _) in basin)
                {
                    isInBasin[y,x] = true;
                }
                
                return basin;
            });
    }

    private Basin FindBasinForPoint(Point point)
    {
        var basin = new Basin();
        basin.AddPoint(point);
        
        var pointsToCheck = new Queue<Point>();
        pointsToCheck.Enqueue(point);
        
        while (pointsToCheck.TryDequeue(out var currentPoint))
        {
            foreach (var neighbourPoint in GetNeighbours(currentPoint))
            {
                if (neighbourPoint.Value != 9 && !basin.Contains(neighbourPoint))
                {
                    basin.AddPoint(neighbourPoint);
                    pointsToCheck.Enqueue(neighbourPoint);
                }
            }
        }
        
        return basin;
    }
    
    private IEnumerable<Point> GetNeighbours(Point point)
    {
        var (x, y, _) = point;
        
        if (_matrix.TryGetTopValue(x, y, out var topValue))
        {
            yield return new Point(x, y - 1, topValue);
        }
        
        if (_matrix.TryGetBottomValue(x, y, out var bottomValue))
        {
            yield return new Point(x, y + 1, bottomValue);
        }
        
        if (_matrix.TryGetLeftValue(x, y, out var leftValue))
        {
            yield return new Point(x - 1, y, leftValue);
        }
        
        if (_matrix.TryGetRightValue(x, y, out var rightValue))
        {
            yield return new Point(x + 1, y, rightValue);
        }
    }
}