namespace AdventOfCode.Day18;

internal class SnailfishValue
{
    private SnailfishValue(int value)
    {
        _value = value;
        _pair = null;
    }

    private SnailfishValue(SnailfishNumber pair)
    {
        _value = null;
        _pair = pair;
    }
    
    private readonly int? _value;
    public int Value => 
        _value ?? throw new InvalidOperationException("this is pair");
    
    private readonly SnailfishNumber? _pair;
    public SnailfishNumber Pair => 
        _pair ?? throw new InvalidOperationException("this is value");
    
    public bool IsValue => _value.HasValue;
    
    public bool IsPair => _pair is not null;

    public override string ToString() =>
        IsValue 
            ? Value.ToString() 
            : Pair.ToString();

    public static SnailfishValue CreateValue(int value) => new(value);
    
    public static SnailfishValue CreatePair(SnailfishNumber pair) => new(pair);
}