namespace AdventOfCode.Day05;

using System.Text.RegularExpressions;

internal static class ParseLineExtension
{
    public static IAsyncEnumerable<Line> ParseLines(this IAsyncEnumerable<string> lines)
    {
        return lines.Select(line =>
        {
            var match = Regex.Match(line, @"(\d+),(\d+) -> (\d+),(\d+)");
    
            var from = new Point(
                int.Parse(match.Groups[1].Value),
                int.Parse(match.Groups[2].Value));
            var to = new Point(
                int.Parse(match.Groups[3].Value), 
                int.Parse(match.Groups[4].Value));
        
            return new Line(from, to);
        });
    }
}