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
        using var calculator = new ProbeTrajectoryCalculator(_startingPosition, startingVelocity);
    
        do
        {
            calculator.CalculateNextPosition();
            var hit = _trench.IsPointInside(calculator.CurrentPosition);

            stepFunction(calculator.CurrentPosition, hit);
            
            if (hit)
            {
                break;
            }
        }
        while (IsHitPossible(calculator.CurrentPosition));
    }
    
    private bool IsHitPossible(Point probePosition) =>
        probePosition.Y >= _trench.MinY &&
        probePosition.X <= _trench.MaxX;
}