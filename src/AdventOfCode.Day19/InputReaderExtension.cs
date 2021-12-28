namespace AdventOfCode.Day19;

internal static class InputReaderExtension
{
    public static async Task<Dictionary<Scanner, HashSet<Point3d>>> ReadScannedPoints(
        this IAsyncEnumerable<string> lines)
    {
        var detectedPositions = new Dictionary<Scanner, HashSet<Point3d>>();

        var currentScanner = new Scanner(-1);

        await foreach (var line in lines)
        {
            if (line.Contains("scanner"))
            {
                var scannerNumber = Regex.Match(line, @"\d+").Value;
                currentScanner = new Scanner(int.Parse(scannerNumber));
        
                detectedPositions.Add(currentScanner, new HashSet<Point3d>());
            }
            else
            {
                var position = line
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();
        
                var vector = new Point3d(position[0], position[1], position[2]);
        
                detectedPositions[currentScanner].Add(vector);
            }
        }
        
        return detectedPositions;
    }
}