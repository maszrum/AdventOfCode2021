namespace AdventOfCode.Common;

public readonly record struct Vector(int X, int Y)
{
    public bool IsZero() => X == 0 && Y == 0;
}