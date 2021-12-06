namespace AdventOfCode.Day05;

internal readonly struct Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }
    
    public int Y { get; }

    public override int GetHashCode()
    {
        unchecked
        {
            var result = X;
            result = 31 * result + Y;
            return result;
        }
    }

    public override string ToString() => $"({X},{Y})";
}