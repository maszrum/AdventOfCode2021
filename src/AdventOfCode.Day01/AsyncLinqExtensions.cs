namespace AdventOfCode.Day01;

internal static class AsyncLinqExtensions
{
    public static async IAsyncEnumerable<TResult> SelectWithPrevious<TSource, TResult>(
        this IAsyncEnumerable<TSource> source, 
        Func<TSource, TSource, TResult> selectorWithPrevious)
    {
        await using var iterator = source.GetAsyncEnumerator();
        
        if (!await iterator.MoveNextAsync())
        {
            yield break;
        }
        
        var previous = iterator.Current;
        
        while (await iterator.MoveNextAsync())
        {
            yield return selectorWithPrevious(previous, iterator.Current);
            previous = iterator.Current;
        }
    }
    
    public static async IAsyncEnumerable<TResult> SelectWithPrevious<TSource, TResult>(
        this IAsyncEnumerable<TSource> source, 
        Func<TSource, TSource, TSource, TResult> selectorWithPrevious)
    {
        await using var iterator = source.GetAsyncEnumerator();
        
        if (!await iterator.MoveNextAsync())
        {
            yield break;
        }
        
        var previous = iterator.Current;
        
        if (!await iterator.MoveNextAsync())
        {
            yield break;
        }
        
        var previousPrevious = iterator.Current;
        
        while (await iterator.MoveNextAsync())
        {
            yield return selectorWithPrevious(previousPrevious, previous, iterator.Current);
            
            previousPrevious = previous;
            previous = iterator.Current;
        }
    }
}