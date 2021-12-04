namespace AdventOfCode.Day04;

internal static class LinqExtensions
{
    public static async IAsyncEnumerable<IReadOnlyList<string>> ChunkByEmptyLine(
        this IAsyncEnumerable<string> lines)
    {
        var list = new List<string>();
        
        await foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                yield return list;
                list = new List<string>();
            }
            else
            {
                list.Add(line);
            }
        }
        
        if (list.Count > 0)
        {
            yield return list;
        }
    }
}