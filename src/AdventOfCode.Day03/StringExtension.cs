namespace AdventOfCode.Day03;

internal static class StringExtension
{
    public static int CountOnes(this string input)
    {
        return input
            .Aggregate(
                seed: 0, 
                func: (onesCounter, character) => character == '1' 
                    ? onesCounter + 1 
                    : onesCounter);
    }
    
    public static string Negate(this string input)
    {
        return input
            .Select(c => c == '0' ? '1' : '0')
            .ToArray()
            .MakeString();
    }
    
    public static string MakeString(this IEnumerable<char> enumerable) => 
        new(enumerable.ToArray());
}