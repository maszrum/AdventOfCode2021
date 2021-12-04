namespace AdventOfCode.Day04;

internal class BingoBoard
{
    private const int BoardSize = 5;
    
    // [row][column]
    private readonly int[][] _numbers;
    private readonly bool[][] _markedNumbers;

    public BingoBoard(IEnumerable<int> numbers)
    {
        _markedNumbers = Enumerable
            .Repeat(0, BoardSize)
            .Select(_ => new bool[BoardSize])
            .ToArray();
        
        _numbers = Enumerable
            .Repeat(0, BoardSize)
            .Select(_ => new int[BoardSize])
            .ToArray();

        var row = 0;
        var column = 0;
        foreach (var number in numbers)
        {
            _numbers[row][column] = number;
            
            column++;
            if (column == BoardSize)
            {
                row++;
                column = 0;
            }
        }
    }
    
    public bool IsWon { get; private set; }
    
    public void Mark(int number)
    {
        var (row, column) = FindPositionOfNumber(number);
        
        if (row >= 0 && column >= 0)
        {
            _markedNumbers[row][column] = true;
            
            if (IsRowWon(row) || IsColumnWon(column))
            {
                IsWon = true;
            }
        }
    }
    
    public IEnumerable<int> GetUnmarkedNumbers()
    {
        return _markedNumbers
            .Select((marks, row) =>
            {
                return marks
                    .Select((isMarked, column) => isMarked 
                        ? (Row: -1, Column: -1) 
                        : (Row: row, Column: column))
                    .Where(position => position.Column >= 0);
            })
            .SelectMany(position => position)
            .Select(position => _numbers[position.Row][position.Column]);
    }

    private (int, int) FindPositionOfNumber(int number)
    {
        for (var row = 0; row < _numbers.Length; row++)
        {
            var rowNumbers = _numbers[row];
            
            var column = Array.FindIndex(rowNumbers, n => n == number);
            if (column >= 0)
            {
                return (row, column);
            }
        }
        return (-1, -1);
    }
    
    private bool IsRowWon(int row) => 
        _markedNumbers[row].All(m => m);

    private bool IsColumnWon(int column) =>
        _markedNumbers
            .Select(r => r[column])
            .All(c => c);
}