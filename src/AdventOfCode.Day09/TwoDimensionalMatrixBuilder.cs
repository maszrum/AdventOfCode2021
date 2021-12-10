namespace AdventOfCode.Day09;

internal class TwoDimensionalMatrixBuilder<T>
{
    private readonly List<IReadOnlyList<T>> _rows = new();
    
    public TwoDimensionalMatrixBuilder<T> AddRow(IEnumerable<T> row)
    {
        _rows.Add(row.ToArray());
        return this;
    }
    
    public TwoDimensionalMatrix<T> Build() => new(_rows.ToArray());
}