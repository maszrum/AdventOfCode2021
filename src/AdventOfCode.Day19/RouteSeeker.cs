namespace AdventOfCode.Day19;

internal class RouteSeeker
{
    private readonly Scanner _destination;

    public RouteSeeker(Scanner destination)
    {
        _destination = destination;
    }
    
    public IEnumerable<MatchedScanners> Find(PossibleRoute start)
    {
        var result = Find(
            current: start, 
            visited: new[] { start });

        for (var i = 1; i < result.Count; i++)
        {
            var previous = result[i - 1].From;
            var current = result[i].From;
            
            yield return new MatchedScanners(previous, current);
        }
    }
    
    // ReSharper disable once PossibleMultipleEnumeration
    private IReadOnlyList<PossibleRoute> Find(
        PossibleRoute current, IEnumerable<PossibleRoute> visited)
    {
        if (current.From == _destination)
        {
            return visited.ToArray();
        }
        
        var nextNodes = current.To
            .Where(r => !visited.Contains(r));

        foreach (var nextNode in nextNodes)
        {
            var result = Find(nextNode, visited.Concat(new [] { nextNode }));
            
            if (result.Count > 0)
            {
                return result;
            }
        }
        
        return Array.Empty<PossibleRoute>();
    }
}