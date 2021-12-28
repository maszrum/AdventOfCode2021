namespace AdventOfCode.Day22;

internal static class InputReaderExtensions
{
    public static IAsyncEnumerable<RebootStep> ReadSteps(this IAsyncEnumerable<string> lines) =>
        lines
            .Select(line => line.StartsWith("on ") 
                ? new RebootStep(true, ReadCuboid(line[3..])) 
                : new RebootStep(false, ReadCuboid(line[4..])));

    private static Cuboid ReadCuboid(string input)
    {
        var parts = input.Split(',');
        
        var xRange = ReadNumbersRange(parts[0][2..]);
        var yRange = ReadNumbersRange(parts[1][2..]);
        var zRange = ReadNumbersRange(parts[2][2..]);
        
        return new Cuboid(
            lowCorner: new LongPoint3d(xRange.A, yRange.A, zRange.A),
            highCorner: new LongPoint3d(xRange.B, yRange.B, zRange.B));
    }
    
    private static Pair<int> ReadNumbersRange(string input)
    {
        var parts = input.Split("..");
        
        return Pair.Create(
            int.Parse(parts[0]), 
            int.Parse(parts[1]));
    }
}