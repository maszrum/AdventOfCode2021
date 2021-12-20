namespace AdventOfCode.Day20;

internal class ImageEnhancer
{
    private readonly IReadOnlyList<bool> _algorithmData;
    
    public ImageEnhancer(
        IReadOnlyList<bool> algorithmData, 
        ImmutableHashSet<Point> input)
    {
        _algorithmData = algorithmData;
        CurrentImage = input;
    }
    
    public bool CouldBeNegative => _algorithmData[0];
    
    public bool IsNegative => _algorithmData[0] && Step % 2 == 1;

    public int Step { get; private set; }
    
    public ImmutableHashSet<Point> CurrentImage { get; private set; }

    public void Enhance()
    {
        Step++;
        
        var output = new HashSet<Point>();
        
        var minX = CurrentImage.Min(point => point.X);
        var maxX = CurrentImage.Max(point => point.X);
        var minY = CurrentImage.Min(point => point.Y);
        var maxY = CurrentImage.Max(point => point.Y);
        
        for (var y = minY - 1; y <= maxY + 1; y++)
        {
            for (var x = minX - 1; x <= maxX + 1; x++)
            {
                var pixel = new Point(x, y);
                
                var neighbourBits = pixel
                    .GetNeighbours(
                        includeDiagonal: true, 
                        includeSelf: true)
                    .OrderBy(point => point.Y)
                    .ThenBy(point => point.X)
                    .Select(point => 
                        IsBorder(point, minX, maxX, minY, maxY) 
                            ? (!IsNegative && CouldBeNegative) 
                            : CurrentImage.Contains(point));
                
                var algorithmIndex = neighbourBits.ToInt32();
                var outputLit = _algorithmData[algorithmIndex];

                if (outputLit)
                {
                    output.Add(pixel);
                }
            }
        }
        
        CurrentImage = output.ToImmutableHashSet();
    }
    
    private static bool IsBorder(Point point, int minX, int maxX, int minY, int maxY) => 
        point.X < minX || point.Y < minY || point.X > maxX || point.Y > maxY;
}