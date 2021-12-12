namespace AdventOfCode.Day12;

internal class Path
{
    public Path(IReadOnlyList<Cave> caves)
    {
        Caves = caves;
    }

    public IReadOnlyList<Cave> Caves { get; }
    
    public bool StartsWith(Path other)
    {
        if (other.Caves.Count > Caves.Count)
        {
            return false;
        }
        
        return !other.Caves
            .Where((cave, i) => cave != Caves[i])
            .Any();
    }

    public Path ExtendWith(params Cave[] caves)
    {
        var concatenatedCaves = Caves
            .Concat(caves)
            .ToList();
        
        return new Path(concatenatedCaves.AsReadOnly());
    }

    public override string ToString() => 
        string.Join(" -> ", Caves);
}