namespace AdventOfCode.Day19;

internal class PointsSetsMatcher
{
    private readonly Dictionary<Scanner, HashSet<Point3d>> _sets;
    private readonly Permutator _permutator = new();
    
    public PointsSetsMatcher(Dictionary<Scanner, HashSet<Point3d>> sets)
    {
        _sets = sets;
    }
    
    public IEnumerable<MatchedScanners> Match()
    {
        var distances = _sets
            .ToDictionary(
                kvp => kvp.Key,
                kvp => _permutator
                    .WithoutRepetitions(kvp.Value, 2)
                    .Select(points => points.ToArray())
                    .Select(points => points[0].DistanceTo(points[1]))
                    .ToList());

        var equalDistances = _permutator
            .WithoutRepetitions(distances, 2)
            .Select(d => d.ToArray())
            .Select(d => 
                new
                {
                    Scanners = new MatchedScanners(d[0].Key, d[1].Key),
                    EqualDistances = d[0].Value.Intersect(d[1].Value).Count()
                })
            .ToArray();

        var maxEqualDistances = equalDistances.Max(d => d.EqualDistances);

        var matchedDetectors = equalDistances
            .Where(d => maxEqualDistances - d.EqualDistances <= 3)
            .Select(d => d.Scanners);
        
        return matchedDetectors;
    }
}