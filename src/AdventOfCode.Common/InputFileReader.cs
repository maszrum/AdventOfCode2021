using System.Collections.ObjectModel;

namespace AdventOfCode.Common;

public class InputFileReader
{
    private readonly List<string> _cachedLines = new();
    
    public InputFileReader(string fileName)
    {
        FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException
                ($"specified file not found: {fileName}", fileName);
        }
    }
    
    public string FileName { get; }
    
    public bool IsFileCached => _cachedLines.Count > 0;

    public Task<ReadOnlyCollection<string>> ReadAllLines()
    {
        if (IsFileCached)
        {
            return Task.FromResult(
                new ReadOnlyCollection<string>(_cachedLines));
        }
        
        return Read();
        
        // local
        async Task<ReadOnlyCollection<string>> Read()
        {
            var lines = await File.ReadAllLinesAsync(FileName);
            _cachedLines.AddRange(lines);
            return new ReadOnlyCollection<string>(lines);
        }
    }

    public async IAsyncEnumerable<string> ReadLineByLine()
    {
        if (IsFileCached)
        {
            foreach (var line in _cachedLines)
            {
                yield return line;
            }
            
            yield break;
        }
        
        using var reader = new StreamReader(FileName);
        
        var readLine = await reader.ReadLineAsync();
        while (readLine is not null)
        {
            _cachedLines.Add(readLine);
            
            yield return readLine;
            
            readLine = await reader.ReadLineAsync();
        }
    }
    
    public Task<string> ReadFirstLine()
    {
        return IsFileCached 
            ? Task.FromResult(_cachedLines[0]) 
            : Read();

        // local
        async Task<string> Read()
        {
            using var reader = new StreamReader(FileName);
        
            var readLine = await reader.ReadLineAsync();
            
            if (readLine is null)
            {
                throw new InvalidOperationException(
                    "file is empty, cannot read first line");
            }
            
            return readLine;
        }
    }
}