namespace AdventOfCode.Day12;

internal class PathTraversal
{
    private readonly ICavesRestriction _restriction;
    private readonly List<Cave> _caves;
    
    public PathTraversal(Cave start, ICavesRestriction restriction)
    {
        _restriction = restriction;
        
        _caves = new List<Cave>
        {
            start
        };
        
        CurrentPath = new Path(_caves.AsReadOnly());
    }
    
    public Cave Current => _caves[^1];
    
    public Path CurrentPath { get; }
    
    public bool TryVisitCave(Cave cave)
    {
        if (cave.IsStart)
        {
            return false;
        }
        
        if (!_restriction.ShouldBeAdded(cave, CurrentPath))
        {
            return false;
        }
        
        _caves.Add(cave);
        return true;
    }
    
    public void StepBack()
    {
        if (_caves.Count == 1)
        {
            throw new InvalidOperationException(
                "cannot step back, you're on start");
        }
        
        _caves.RemoveAt(_caves.Count - 1);
    }
    
    public Path Build() => new(new List<Cave>(_caves).AsReadOnly());

    public override string ToString() => CurrentPath.ToString();
}