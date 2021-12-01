namespace AdventOfCode.Common;

public sealed class FileLineByLineReader : IAsyncEnumerable<string>
{
    private readonly string _fileName;
    
    public FileLineByLineReader(string fileName)
    {
        _fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException
                ($"specified file not found: {fileName}", fileName);
        }
    }

    public IAsyncEnumerator<string> GetAsyncEnumerator(
        CancellationToken cancellationToken = new())
    {
        return new LineByLineEnumerator(_fileName);
    }
}