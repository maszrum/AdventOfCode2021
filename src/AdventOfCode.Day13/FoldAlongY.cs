namespace AdventOfCode.Day13;

internal class FoldAlongY : IFoldingInstruction
{
    private readonly int _y;

    public FoldAlongY(int y)
    {
        _y = y;
    }

    public IReadOnlyList<Point> Fold(IReadOnlyList<Point> points)
    {
        var foldedPoints = points
            .Where(p => p.Y > _y)
            .Select(p => p.ToDown(2 * (p.Y - _y)))
            .Where(p => !points.Contains(p));
    
        return points
            .Where(p => p.Y <= _y)
            .Concat(foldedPoints)
            .ToArray();
    }
}