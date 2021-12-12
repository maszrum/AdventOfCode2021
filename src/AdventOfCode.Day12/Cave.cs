namespace AdventOfCode.Day12;

internal class Cave
{
    private IEnumerable<Cave>? _connections;

    public Cave(string name)
    {
        Name = name;
        IsSmall = name.ToLower() == name;
        IsStart = Name == "start";
        IsEnd = Name == "end";
    }

    public IEnumerable<Cave> Connections => 
        _connections ?? throw new InvalidOperationException(
            $"set connections by {nameof(SetupConnections)} method");

    public string Name { get; }
    
    public bool IsSmall { get; }
    
    public bool IsStart { get; }
    
    public bool IsEnd { get; }
    
    public void SetupConnections(IEnumerable<Cave> connections)
    {
        if (_connections is not null)
        {
            throw new InvalidOperationException(
                "method could be called only once");
        }
        
        _connections = connections;
    }

    public override string ToString() => Name;
}