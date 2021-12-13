namespace AdventOfCode.Common;

public class RowMatrixBuilder<T>
{
    private readonly List<T[]> _rows = new();
    
    public RowMatrixBuilder<T> AddRow(IEnumerable<T> row)
    {
        _rows.Add(row.ToArray());
        return this;
    }
    
    public Matrix<T> Build() => new(_rows.ToArray());
    
    public MutableMatrix<T> BuildMutable() => new(_rows.ToArray());
}