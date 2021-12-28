namespace AdventOfCode.Common;

public static class VectorExtensions
{
    public static Point3d ToPoint(this Vector3d vector) => 
        new(vector.X, vector.Y, vector.Z);
}