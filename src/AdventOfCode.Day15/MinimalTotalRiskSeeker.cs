namespace AdventOfCode.Day15;

internal class MinimalTotalRiskSeeker
{
    private readonly Matrix<int> _map;

    public MinimalTotalRiskSeeker(Matrix<int> map)
    {
        _map = map;
    }

    public int Find(Point start, Point end)
    {
        var riskMap = new CoordinateMatrixBuilder<int>()
            .WithDefaultValue(int.MaxValue)
            .AddPoint(
                new Point(_map.Columns - 1, _map.Rows - 1)
                    .WithValue(int.MaxValue))
            .BuildMutable();
        
        var queue = new Queue<PointWithValue<int>>();
        queue.Enqueue(start.WithValue(0));
        
        while (queue.TryDequeue(out var point))
        {
            var neighbours = _map
                .GetNeighbours(point)
                .Select(n => n.WithoutValue());

            foreach (var neighbour in neighbours)
            {
                var risk = riskMap[neighbour];
                var newRisk = point.Value + _map[neighbour];
                
                if (newRisk < risk)
                {
                    riskMap[neighbour] = newRisk;
                    queue.Enqueue(neighbour.WithValue(newRisk));
                }
            }
        }
        
        return riskMap[end];
    }
}