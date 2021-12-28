namespace AdventOfCode.Common;

public readonly record struct Vector3d(int X, int Y, int Z)
{
    public static Vector3d Zero => new(0, 0, 0);
    
    public static bool IsZero(Vector3d vector) => 
        vector.X == 0 && vector.Y == 0 && vector.Z == 0;
    
    public static Vector3d operator +(Vector3d a, Vector3d b) => 
        new(
            X: a.X + b.X, 
            Y: a.Y + b.Y, 
            Z: a.Z + b.Z);
}