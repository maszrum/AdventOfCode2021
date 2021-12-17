namespace AdventOfCode.Day17;

internal class ProbeTrajectoryCalculator : IDisposable
{
    private Vector _currentVelocity;
    private IEnumerator<Point>? _enumerator;

    public ProbeTrajectoryCalculator(
        Point startPosition, 
        Vector startVelocity)
    {
        CurrentPosition = startPosition;
        _currentVelocity = startVelocity;
    }
    
    public Point CurrentPosition { get; private set; }
    
    public void CalculateNextPosition()
    {
        _enumerator ??= CalculateSubsequentPositions()
            .GetEnumerator();
        
        _enumerator.MoveNext();
        CurrentPosition = _enumerator.Current;
    }

    private IEnumerable<Point> CalculateSubsequentPositions()
    {
        yield return CurrentPosition;
        
        while (true)
        {
            CurrentPosition = new Point(
                X: CurrentPosition.X + _currentVelocity.X,
                Y: CurrentPosition.Y + _currentVelocity.Y);
            
            yield return CurrentPosition;
            
            _currentVelocity = new Vector(
                X: _currentVelocity.X - Math.Sign(_currentVelocity.X),
                Y: _currentVelocity.Y - 1);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    public void Dispose()
    {
        _enumerator?.Dispose();
    }
}