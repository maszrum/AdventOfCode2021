namespace AdventOfCode.Common;

public class TwoDimensionalMatrixBuilder<T>
{
    private readonly List<T[]> _rows = new();
    
    public TwoDimensionalMatrixBuilder<T> AddRow(IEnumerable<T> row)
    {
        _rows.Add(row.ToArray());
        return this;
    }
    
    public TwoDimensionalMatrix<T> Build() => new(_rows.ToArray());
    
    public MutableTwoDimensionalMatrix<T> BuildMutable() => new(_rows.ToArray());
}