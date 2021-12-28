namespace AdventOfCode.Common;

public static class Point3dExtensions
{
    public static double DistanceTo(this Point3d a, Point3d b)
    {
        var deltaX = a.X - b.X;
        var deltaY = a.Y - b.Y;
        var deltaZ = a.Z - b.Z;
        
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
    }
}