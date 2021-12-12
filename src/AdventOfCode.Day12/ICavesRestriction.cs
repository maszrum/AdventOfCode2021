namespace AdventOfCode.Day12;

internal interface ICavesRestriction
{
    bool ShouldBeAdded(Cave cave, Path currentPath);
}