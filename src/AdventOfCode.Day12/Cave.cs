namespace AdventOfCode.Day12;

internal class Cave
{
    private IReadOnlyCollection<Cave>? _connections;

    public Cave(string name)
    {
        Name = name;
        IsSmall = name.ToLower() == name;
        IsStart = Name == "start";
        IsEnd = Name == "end";
    }

    public IReadOnlyCollection<Cave> Connections => 
        _connections ?? throw new InvalidOperationException(
            $"set connections by {nameof(SetConnections)} method");

    public string Name { get; }
    
    public bool IsSmall { get; }
    
    public bool IsStart { get; }
    
    public bool IsEnd { get; }
    
    public void SetConnections(IEnumerable<Cave> connections)
    {
        if (_connections is not null)
        {
            throw new InvalidOperationException(
                "method could be called only once");
        }
        
        _connections = connections.ToArray();
    }

    public override string ToString() => Name;
}