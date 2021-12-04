var reader = new InputFileReader("input.txt");

/* instantiate boards */

var boards = await reader
    .ReadLineByLine()
    .Skip(2)
    .ChunkByEmptyLine()
    .Select(numberLines =>
    {
        var numbers = numberLines
            .Select(line => 
                line
                    .Split(' ')
                    .Select(number => number.Trim())
                    .Where(number => !string.IsNullOrEmpty(number))
                    .Select(int.Parse))
            .SelectMany(number => number);
        
        return new BingoBoard(numbers);
    })
    .ToArrayAsync();

/* read input numbers sequence */

var inputNumbers = (await reader.ReadFirstLine())
    .Split(',')
    .Where(number => !string.IsNullOrEmpty(number))
    .Select(int.Parse)
    .ToArray();

/*
 * Part one
 */

foreach (var inputNumber in inputNumbers)
{
    var winner = boards
        .Select(board =>
        {
            board.Mark(inputNumber);
            return board;
        })
        .SkipWhile(board => !board.IsWon)
        .Take(1)
        .SingleOrDefault();
    
    if (winner is not null)
    {
        var unmarkedNumbersSum = winner.GetUnmarkedNumbers().Sum();
        var finalScore = unmarkedNumbersSum * inputNumber;
        
        Console.WriteLine($"Final score of the first winning bingo board is {finalScore}");
        break;
    }
}

/*
 * Part two
 */

boards = boards
    .Where(b => !b.IsWon)
    .ToArray();

foreach (var inputNumber in inputNumbers)
{
    if (boards.Length > 1)
    {
        boards = boards
            .Select(board =>
            {
                board.Mark(inputNumber);
                return board;
            })
            .Where(b => !b.IsWon)
            .ToArray();
    }
    else
    {
        var loserBoard = boards[0];
        loserBoard.Mark(inputNumber);
        
        if (loserBoard.IsWon)
        {
            var unmarkedNumbersSum = loserBoard.GetUnmarkedNumbers().Sum();
            var finalScore = unmarkedNumbersSum * inputNumber;
            
            Console.WriteLine($"Final score of the last winning bingo board is {finalScore}");
            break;
        }
    }
}