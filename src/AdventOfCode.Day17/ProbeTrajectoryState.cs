namespace AdventOfCode.Day17;

internal class ProbeTrajectoryState
{
    public ProbeTrajectoryState(
        Point startPosition, 
        Vector startVelocity)
    {
        CurrentPosition = startPosition;
        CurrentVelocity = startVelocity;
    }
    
    public Point CurrentPosition { get; private set; }
 
    public Vector CurrentVelocity { get; private set; }
    
    public void MoveToNextPosition()
    {
        CurrentPosition = new Point(
            X: CurrentPosition.X + CurrentVelocity.X,
            Y: CurrentPosition.Y + CurrentVelocity.Y);
        
        CurrentVelocity = new Vector(
            X: CurrentVelocity.X - Math.Sign(CurrentVelocity.X),
            Y: CurrentVelocity.Y - 1);
    }
}