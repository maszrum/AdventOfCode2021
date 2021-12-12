namespace AdventOfCode.Day12;

internal class PartTwoCavesRestriction : ICavesRestriction
{
    public bool ShouldBeVisited(Cave cave, Path currentPath)
    {
        if (!cave.IsSmall || !currentPath.Caves.Contains(cave))
        {
            return true;
        }
        
        var smallCavesCount = currentPath.Caves
            .Count(c => c.IsSmall);
        
        var smallCavesDistinctCount = currentPath.Caves
            .Where(c => c.IsSmall)
            .Distinct()
            .Count();
        
        return smallCavesCount == smallCavesDistinctCount;
    }
}