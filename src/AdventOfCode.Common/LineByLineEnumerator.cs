namespace AdventOfCode.Common;

public sealed class LineByLineEnumerator : IAsyncEnumerator<string>
{
    private readonly string _fileName;
    
    private StreamReader? _reader;
    private string? _current;

    public LineByLineEnumerator(string fileName)
    {
        _fileName = fileName;
    }

    public string Current => _current ?? throw new InvalidOperationException("current is null");

    public async ValueTask<bool> MoveNextAsync()
    {
        _reader ??= new StreamReader(_fileName);
        
        _current = await _reader.ReadLineAsync();
        
        return _current is not null;
    }
    
    public ValueTask DisposeAsync()
    {
        _reader?.Close();
        return ValueTask.CompletedTask;
    }
}