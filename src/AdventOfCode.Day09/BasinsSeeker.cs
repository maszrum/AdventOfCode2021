namespace AdventOfCode.Day09;

internal class BasinsSeeker
{
    private readonly Matrix<int> _matrix;
    
    public BasinsSeeker(Matrix<int> matrix)
    {
        _matrix = matrix;
    }

    public IEnumerable<Basin> Find(IEnumerable<PointWithValue<int>> minima)
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

    private Basin FindBasinForPoint(PointWithValue<int> point)
    {
        var basin = new Basin();
        basin.AddPoint(point);
        
        var pointsToCheck = new Queue<PointWithValue<int>>();
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
    
    private IEnumerable<PointWithValue<int>> GetNeighbours(PointWithValue<int> point)
    {
        var (x, y, _) = point;
        
        if (_matrix.TryGetTopValue(x, y, out var topValue))
        {
            yield return new PointWithValue<int>(x, y - 1, topValue);
        }
        
        if (_matrix.TryGetBottomValue(x, y, out var bottomValue))
        {
            yield return new PointWithValue<int>(x, y + 1, bottomValue);
        }
        
        if (_matrix.TryGetLeftValue(x, y, out var leftValue))
        {
            yield return new PointWithValue<int>(x - 1, y, leftValue);
        }
        
        if (_matrix.TryGetRightValue(x, y, out var rightValue))
        {
            yield return new PointWithValue<int>(x + 1, y, rightValue);
        }
    }
}