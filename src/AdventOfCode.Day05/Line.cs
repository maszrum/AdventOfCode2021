namespace AdventOfCode.Day05;

internal readonly struct Line
{
    public Line(Point from, Point to)
    {
        From = from;
        To = to;
    }

    public Point From { get; }
    
    public Point To { get; }
    
    public bool IsHorizontal => From.Y == To.Y;
    
    public bool IsVertical => From.X == To.X;
    
    public bool IsDiagonal => Math.Abs(From.Y - To.Y) == Math.Abs(From.X - To.X);
    
    public IEnumerable<Point> GetPointsBetween()
    {
        var thisCopy = this;
        
        if (IsHorizontal)
        {
            return Enumerable
                .Range(
                    start: Math.Min(thisCopy.From.X, thisCopy.To.X), 
                    count: Math.Abs(thisCopy.From.X - thisCopy.To.X) + 1)
                .Select(x => new Point(x, thisCopy.From.Y));
        }
        if (IsVertical)
        {
            return Enumerable
                .Range(
                    start: Math.Min(thisCopy.From.Y, thisCopy.To.Y),
                    count: Math.Abs(thisCopy.From.Y - thisCopy.To.Y) + 1)
                .Select(y => new Point(thisCopy.From.X, y));
        }
        if (IsDiagonal)
        {
            var xSign = Math.Sign(thisCopy.To.X - thisCopy.From.X);
            var ySign = Math.Sign(thisCopy.To.Y - thisCopy.From.Y);
            
            return Enumerable
                .Range(
                    start: 0,
                    count: Math.Abs(thisCopy.From.Y - thisCopy.To.Y) + 1)
                .Select(i => new Point(thisCopy.From.X + i * xSign, thisCopy.From.Y + i * ySign));
        }
        
        throw new InvalidOperationException(
            "line must be vertical, horizontal or diagonal");
    }

    public override string ToString() => $"{From} -> {To}";
}