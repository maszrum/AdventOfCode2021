namespace AdventOfCode.Day09;

internal class TwoDimensionalMatrixBuilder<T>
{
    private readonly List<IReadOnlyList<T>> _rows = new();
    
    public void AddRow(IEnumerable<T> row)
    {
        _rows.Add(row.ToArray());
    }
    
    public TwoDimensionalMatrix<T> Build() => new(_rows.ToArray());
}