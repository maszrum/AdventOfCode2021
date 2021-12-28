namespace AdventOfCode.Common;

public readonly record struct Point3d(int X, int Y, int Z)
{
    public static Vector3d operator -(Point3d a, Point3d b) =>
        new(
            X: a.X - b.X,
            Y: a.Y - b.Y,
            Z: a.Z - b.Z);
    
    public static Point3d operator +(Point3d a, Point3d b) => 
        new(
            X: a.X + b.X,
            Y: a.Y + b.Y,
            Z: a.Z + b.Z);
}