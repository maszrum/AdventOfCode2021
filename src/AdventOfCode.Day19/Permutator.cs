namespace AdventOfCode.Day19;

// ReSharper disable PossibleMultipleEnumeration

internal class Permutator
{
    public IEnumerable<IEnumerable<T>> WithRepetitions<T>(IEnumerable<T> items, int count)
    {
        if (count == 1)
        {
            return items.Select(item => new[] { item });
        }
        
        return WithRepetitions(items, count - 1)
            .SelectMany(_ => items, 
                (i1, i2) => i1.Concat(new[] { i2 }));
    }
    
    public IEnumerable<IEnumerable<T>> WithoutRepetitions<T>(IEnumerable<T> items, int count)
    {
        var i = 0;
        
        foreach (var item in items)
        {
            if (count == 1)
            {
                yield return new[] { item };
            }
            else
            {
                var otherElements = WithoutRepetitions(
                    items: items.Skip(i + 1), 
                    count: count - 1);
                
                foreach (var result in otherElements)
                {
                    yield return new[] { item }
                        .Concat(result);
                }
            }

            i++;
        }
    }
}