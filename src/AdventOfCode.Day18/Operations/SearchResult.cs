namespace AdventOfCode.Day18.Operations;

internal class SearchResult
{
    private SearchResult()
    {
        _number = null;
        _value = null;
    }
    
    private SearchResult(
        SnailfishNumber number, 
        SnailfishValue value)
    {
        _number = number;
        _value = value;
    }
    
    private readonly SnailfishNumber? _number;
    public SnailfishNumber Number => 
        _number ?? throw new InvalidOperationException("search result is empty");
    
    private readonly SnailfishValue? _value;

    public SnailfishValue Value => 
        _value ?? throw new InvalidOperationException("search result is empty");
    
    public bool Found => _value is not null;
    
    public bool InLeft => _value == Number.Left;
    
    public bool InRight => _value == Number.Right;
    
    public static SearchResult NotFound() => new();
    
    public static SearchResult FoundInLeft(SnailfishNumber number) => 
        new(number, number.Left);
    
    public static SearchResult FoundInRight(SnailfishNumber number) => 
        new(number, number.Right);
}