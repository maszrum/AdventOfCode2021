namespace AdventOfCode.Day12;

internal class PartOneCavesRestriction : ICavesRestriction
{
    public bool ShouldBeAdded(Cave cave, Path currentPath) => 
        !cave.IsSmall || !currentPath.Caves.Contains(cave);
}