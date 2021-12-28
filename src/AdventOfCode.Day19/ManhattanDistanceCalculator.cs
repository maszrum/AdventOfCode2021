namespace AdventOfCode.Day19;

internal class ManhattanDistanceCalculator
{
    public int Calculate(Point3d a, Point3d b)
    {
        return Math.Abs(a.X - b.X) + 
               Math.Abs(a.Y - b.Y) + 
               Math.Abs(a.Z - b.Z);
    }
}