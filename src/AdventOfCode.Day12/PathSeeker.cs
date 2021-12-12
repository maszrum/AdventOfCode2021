using System.Collections.Immutable;

namespace AdventOfCode.Day12;

internal class PathSeeker
{
    private readonly ICavesRestriction _restriction;

    public PathSeeker(ICavesRestriction restriction)
    {
        _restriction = restriction;
    }

    public ImmutableHashSet<Path> Find(Cave start)
    {
        var traversal = new PathTraversal(start, _restriction);
        
        var visitedPaths = new HashSet<Path>();
        var validPaths = new HashSet<Path>();
        
        while (true)
        {
            if (traversal.Current.IsEnd)
            {
                var path = traversal.Build();
                validPaths.Add(path);
                visitedPaths.Add(path);
                
                traversal.StepBack();
            }
            
            var current = traversal.Current;
            
            var nextCaves = current.Connections
                .Where(cave =>
                {
                    var newPath = traversal.CurrentPath.ExtendWith(cave);
                    return visitedPaths.All(p => !p.StartsWith(newPath));
                });
            
            var visitedNext = false;
            
            foreach (var nextCave in nextCaves)
            {
                if (traversal.TryVisitCave(nextCave))
                {
                    visitedNext = true;
                    break;
                }
                var invalidPath = traversal.CurrentPath.ExtendWith(nextCave);
                visitedPaths.Add(invalidPath);
            }
            
            if (!visitedNext)
            {
                if (current.IsStart)
                {
                    break; // exit loop, no more caves to explore
                }
                
                visitedPaths.RemoveWhere(p => p.StartsWith(traversal.CurrentPath));
                visitedPaths.Add(traversal.Build());
                
                traversal.StepBack();
            }
        }
        
        return validPaths.ToImmutableHashSet();
    }
}