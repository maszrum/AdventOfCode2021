namespace AdventOfCode.Day13;

internal class FoldAlongX : IFoldingInstruction
{
    private readonly int _x;

    public FoldAlongX(int x)
    {
        _x = x;
    }

    public IReadOnlyList<Point> Fold(IReadOnlyList<Point> points)
    {
        var foldedPoints = points
            .Where(p => p.X > _x)
            .Select(p => p.ToLeft(2 * (p.X - _x)))
            .Where(p => !points.Contains(p));
    
        return points
            .Where(p => p.X <= _x)
            .Concat(foldedPoints)
            .ToArray();
    }
}