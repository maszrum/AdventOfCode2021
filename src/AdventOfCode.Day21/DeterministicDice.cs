namespace AdventOfCode.Day21;

internal class DeterministicDice
{
    private int _counter;
    
    public int TotalRolls { get; private set; }
    
    public int Roll()
    {
        _counter++;
        
        if (_counter > 100)
        {
            _counter = 1;
        }
        
        TotalRolls++;
        
        return _counter;
    }
}