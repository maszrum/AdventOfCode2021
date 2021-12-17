namespace AdventOfCode.Day17;

internal class ShootSimulation
{
    private readonly Point _startingPosition;
    private readonly OceanTrench _trench;

    public ShootSimulation(Point startingPosition, OceanTrench trench)
    {
        _startingPosition = startingPosition;
        _trench = trench;
    }
    
    public void SimulateSteps(Vector startingVelocity, Action<Point, bool> stepFunction)
    {
        var probeState = new ProbeTrajectoryState(_startingPosition, startingVelocity);
    
        do
        {
            probeState.MoveToNextPosition();
            var hit = _trench.IsPointInside(probeState.CurrentPosition);

            stepFunction(probeState.CurrentPosition, hit);
            
            if (hit)
            {
                break;
            }
        }
        while (IsHitPossible(probeState.CurrentPosition));
    }
    
    private bool IsHitPossible(Point probePosition) =>
        probePosition.Y >= _trench.MinY &&
        probePosition.X <= _trench.MaxX;
}