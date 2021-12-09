namespace AdventOfCode.Day09;

internal class LocalMinimaSeeker
{
    public IReadOnlyList<int> FindMinima(IReadOnlyList<int> values)
    {
        var localMinima = new List<int>();
    
        // check first
        if (values[0] < values[1])
        {
            localMinima.Add(0);
        }
    
        for (var i = 1; i < values.Count - 1; i++)
        {
            var previous = values[i - 1];
            var current = values[i];
            var next = values[i + 1];
        
            if (current < previous && current < next)
            {
                localMinima.Add(i);
            }
        }
    
        // check last
        if (values[^1] < values[^2])
        {
            localMinima.Add(values.Count - 1);
        }
    
        return localMinima;
    }
}