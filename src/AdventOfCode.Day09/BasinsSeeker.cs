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
            var neighbours = _matrix.GetNeighbours(currentPoint)
                .Where(neighbour => !basin.Contains(neighbour));
            
            foreach (var neighbourPoint in neighbours)
            {
                if (neighbourPoint.Value != 9)
                {
                    basin.AddPoint(neighbourPoint);
                    pointsToCheck.Enqueue(neighbourPoint);
                }
            }
        }
        
        return basin;
    }
}