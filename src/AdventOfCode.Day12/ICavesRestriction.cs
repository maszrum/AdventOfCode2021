namespace AdventOfCode.Day12;

internal interface ICavesRestriction
{
    bool ShouldBeVisited(Cave cave, Path currentPath);
}