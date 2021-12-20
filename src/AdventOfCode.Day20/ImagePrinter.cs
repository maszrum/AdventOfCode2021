namespace AdventOfCode.Day20;

internal class ImagePrinter
{
    public IEnumerable<string> Print(ImmutableHashSet<Point> imagePixels)
    {
        var minX = imagePixels.Min(point => point.X);
        var maxX = imagePixels.Max(point => point.X);
        var minY = imagePixels.Min(point => point.Y);
        var maxY = imagePixels.Max(point => point.Y);
        
        for (var y = minY; y <= maxY; y++)
        {
            var line = new StringBuilder();
            
            for (var x = minX; x <= maxX; x++)
            {
                var character = imagePixels.Contains(new Point(x, y))
                    ? '#' : '.';
                line.Append(character);
            }
            
            yield return line.ToString();
        }
    }
    
    public void Print(
        ImmutableHashSet<Point> imagePixels, Action<string> lineFunction)
    {
        foreach (var line in Print(imagePixels))
        {
            lineFunction(line);
        }
    }
}