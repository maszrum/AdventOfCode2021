namespace AdventOfCode.Day13;

internal interface IFoldingInstruction
{
    IReadOnlyList<Point> Fold(IReadOnlyList<Point> points);
}