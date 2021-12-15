namespace AdventOfCode.Day15;

internal class MapExtender
{
    private readonly Matrix<int> _inputMatrix;
    private readonly int _tileWidth;
    private readonly int _tileHeight;

    public MapExtender(Matrix<int> inputMatrix)
    {
        _inputMatrix = inputMatrix;
        _tileWidth = _inputMatrix.Columns;
        _tileHeight = _inputMatrix.Rows;
    }

    public Matrix<int> Extend(int extendTimes)
    {
        var matrixBuilder = new CoordinateMatrixBuilder<int>();
        
        for (var y = 0; y < _tileHeight; y++)
        {
            for (var x = 0; x < _tileWidth; x++)
            {
                var originalPoint = new PointWithValue<int>(x, y, _inputMatrix.GetValue(x, y));
                
                var extraPoints = ExtendPoint(originalPoint, extendTimes);
                
                matrixBuilder.AddPoints(extraPoints);
            }
        }
        
        return matrixBuilder.Build();
    }
    
    private IEnumerable<PointWithValue<int>> ExtendPoint(PointWithValue<int> point, int extendTimes)
    {
        return Enumerable.Range(0, extendTimes)
            .SelectMany(tX => 
                Enumerable.Range(0, extendTimes)
                    .Select(tY =>
                    {
                        var addValue = tX + tY;
                        var (originalX, originalY, originalValue) = point;
                        
                        var newValue = (originalValue + addValue);
                        if (newValue > 9)
                            newValue %= 9;

                        return new PointWithValue<int>(
                            X: originalX + _tileWidth * tX,
                            Y: originalY + _tileHeight * tY,
                            Value: newValue);
                    }));
    }
}