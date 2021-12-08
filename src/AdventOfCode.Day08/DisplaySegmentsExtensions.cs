namespace AdventOfCode.Day08;

internal static class DisplaySegmentsExtensions
{
    private static readonly Dictionary<char, DisplaySegments> ConversionDictionary = new()
    {
        ['a'] = DisplaySegments.A,
        ['b'] = DisplaySegments.B,
        ['c'] = DisplaySegments.C,
        ['d'] = DisplaySegments.D,
        ['e'] = DisplaySegments.E,
        ['f'] = DisplaySegments.F,
        ['g'] = DisplaySegments.G
    };

    public static DisplaySegments ToDisplaySegments(this string segmentsText)
    {
        return segmentsText
            .Aggregate(
                DisplaySegments.None, 
                (current, character) => current | ConversionDictionary[character]);
    }
    
    public static int CountSegments(this DisplaySegments displaySegments)
    {
        var count = 0;

        while (displaySegments != 0)
        {
            displaySegments &= (displaySegments - 1);
            count++;
        }

        return count;
    }
}