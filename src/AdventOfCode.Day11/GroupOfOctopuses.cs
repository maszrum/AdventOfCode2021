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
    
    private IEnumerable<Point> GetChargedOctopuses() =>
        _energyLevels.MatrixWhere(level => level >= EnergyThatMakesOctopusFlash);
    
    private void TriggerChainReaction(IReadOnlyList<Point> octopuses, Action onFlash)
    {
        var octopusJustFlashed = new List<Point>(octopuses);
        
        var queue = new Queue<Point>(octopuses);
        
        while (queue.TryDequeue(out var octopus))
        {
            var neighbours = _energyLevels
                .GetNeighbours(
                    point: octopus,
                    includeDiagonal: true)
                .Select(n => new Point(n.X, n.Y))
                .Where(n => !octopusJustFlashed.Contains(n));
            
            foreach (var neighbour in neighbours)
            {
                var energyLevel = ++_energyLevels[neighbour];
                
                if (energyLevel >= EnergyThatMakesOctopusFlash)
                {
                    onFlash();
                    
                    queue.Enqueue(neighbour);
                    octopusJustFlashed.Add(neighbour);
                }
            }
        }

        foreach (var octopus in octopusJustFlashed)
        {
            _energyLevels[octopus] = 0;
        }
    }

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