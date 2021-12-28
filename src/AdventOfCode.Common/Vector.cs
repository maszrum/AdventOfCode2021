namespace AdventOfCode.Common;

public readonly record struct Vector(int X, int Y)
{
    public static bool IsZero(Vector vector) => 
        vector.X == 0 && vector.Y == 0;
}