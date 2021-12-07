namespace AdventOfCode.Day07;

internal class MinimalCostSolver
{
    private readonly Func<int, int, int> _costFunction;

    public MinimalCostSolver(Func<int, int, int> costFunction)
    {
        _costFunction = costFunction;
    }

    public int FindMinimalCost(IReadOnlyList<int> positions)
    {
        var min = positions.Min();
        var max = positions.Max();
    
        var minimalCost = int.MaxValue;

        for (var position = min; position <= max; position++)
        {
            var cost = positions
                .Sum(p => _costFunction(p, position));
        
            minimalCost = Math.Min(minimalCost, cost);
        }
    
        return minimalCost;
    }
}