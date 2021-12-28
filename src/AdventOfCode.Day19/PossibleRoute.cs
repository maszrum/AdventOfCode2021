namespace AdventOfCode.Day19;

internal class PossibleRoute
{
    public PossibleRoute(Scanner @from)
    {
        From = @from;
    }

    public Scanner From { get; }
    
    private IReadOnlyList<PossibleRoute>? _routes;
    public IReadOnlyList<PossibleRoute> To => 
        _routes ?? throw new InvalidOperationException($"call {nameof(SetupRoutes)} before");
    
    public PossibleRoute SetupRoutes(IReadOnlyList<PossibleRoute> routes)
    {
        _routes = routes;
        return this;
    }

    public override string ToString() => From.ToString();
}