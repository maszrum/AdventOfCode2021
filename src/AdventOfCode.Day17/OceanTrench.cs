namespace AdventOfCode.Day17;

internal class OceanTrench
{
    public OceanTrench(int minX, int maxX, int minY, int maxY)
    {
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;
    }

    public int MinX { get; }
    
    public int MaxX { get; }
    
    public int MinY { get; }
    
    public int MaxY { get; }
    
    public bool IsPointInside(Point point) =>
        point.X >= MinX && point.X <= MaxX &&
        point.Y >= MinY && point.Y <= MaxY;
}