using System.Collections.ObjectModel;

namespace AdventOfCode.Common;

public class LinesToColumnsConverter
{
    public ReadOnlyCollection<string> Convert(IReadOnlyCollection<string> lines)
    {
        var lineLength = lines.First().Length;
        
        if (lines.Any(l => l.Length != lineLength))
        {
            throw new InvalidOperationException(
                "lines read from the file are of unequal length");
        }
        
        if (lines.Count == 0 || lines.First().Length == 0)
        {
            return new ReadOnlyCollection<string>(
                Array.Empty<string>());
        }
        
        var result = lines.First()
            .Select((_, index) =>
            {
                var chars = lines
                    .Select(l => l[index])
                    .ToArray();
                return new string(chars);
            })
            .ToArray();
        
        return new ReadOnlyCollection<string>(result);
    }
    
    public string GetSingleColumn(IEnumerable<string> lines, int columnIndex) =>
        new(lines.Select(l => l[columnIndex]).ToArray());
}