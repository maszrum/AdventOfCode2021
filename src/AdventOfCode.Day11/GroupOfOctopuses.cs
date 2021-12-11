using System.Text;

namespace AdventOfCode.Day11;

public class GroupOfOctopuses
{
    private const int EnergyThatMakesOctopusFlash = 10;
    
    private readonly MutableMatrix<int> _energyLevels;

    public GroupOfOctopuses(MutableMatrix<int> energyLevels)
    {
        _energyLevels = energyLevels;
    }
    
    public bool IsSynchronizedFlash => Flashes == _energyLevels.Columns * _energyLevels.Rows;
    
    public int Flashes { get; private set; }
    
    public int StepNumber { get; private set; }
    
    public int Step()
    {
        _energyLevels.IncrementEvery();

        var chargedOctopuses = GetChargedOctopuses().ToArray();
        var flashes = chargedOctopuses.Length;
        
        TriggerChainReaction(
            octopuses: chargedOctopuses, 
            onFlash: () => flashes++);
        
        StepNumber++;
        Flashes = flashes;
        
        return flashes;
    }
    
    private void TriggerChainReaction(IReadOnlyList<Point> octopuses, Action onFlash)
    {
        var octopusJustFlashed = new List<Point>(octopuses);
        
        var queue = new Queue<Point>(octopuses);
        
        while (queue.TryDequeue(out var octopus))
        {
            var neighbours = _energyLevels
                .GetNeighbours(
                    x: octopus.X, 
                    y: octopus.Y, 
                    includeDiagonal: true)
                .Select(n => new Point(n.X, n.Y));
            
            var neighboursNotFlashed = neighbours
                .Where(n => !octopusJustFlashed.Contains(n));

            foreach (var neighbour in neighboursNotFlashed)
            {
                var energyLevel = _energyLevels.Increment(neighbour.X, neighbour.Y);
                
                if (energyLevel >= EnergyThatMakesOctopusFlash)
                {
                    onFlash();
                    
                    queue.Enqueue(neighbour);
                    
                    octopusJustFlashed.Add(neighbour);
                }
            }
        }

        foreach (var (x, y) in octopusJustFlashed)
        {
            _energyLevels.SetValue(x, y, 0);
        }
    }
    
    private IEnumerable<Point> GetChargedOctopuses() =>
        _energyLevels.MatrixWhere(level => level >= EnergyThatMakesOctopusFlash);

    public override string ToString() => 
        _energyLevels
            .Aggregate(
                seed: new StringBuilder(), 
                func: (stringBuilder, row) => row.
                    Aggregate(
                        seed: stringBuilder.Length > 0 ? stringBuilder.AppendLine() : stringBuilder, 
                        func: (sb, value) => sb.Append(value)))
            .ToString();
}