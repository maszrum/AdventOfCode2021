var reader = new InputFileReader("input.txt");

/*
 * Part one
 */

var bracketPoints = new Dictionary<char, int>
{
    [')'] = 3,
    [']'] = 57,
    ['}'] = 1197,
    ['>'] = 25137,
};

var corruptedLinesTotalScore = await reader.ReadLineByLine()
    .Select(line =>
    {
        var checker = new BracketSyntaxChecker(line);
        return !checker.TryGetNextError(out var index) 
            ? ' ' 
            : line[index];
    })
    .Where(character => character != ' ')
    .AggregateAsync(0, (totalScore, bracket) => totalScore + bracketPoints[bracket]);
    
Console.WriteLine($"The total syntax error score is {corruptedLinesTotalScore}.");

/*
 * Part two
 */

var scoreCalculator = new AutocompletionScoreCalculator();

var scoresAscending = await reader.ReadLineByLine()
    .Select(line =>
    {
        var checker = new BracketSyntaxChecker(line);
        return checker.TryGetNextError(out _)
            ? string.Empty 
            : checker.GetMissingPart();
    })
    .Where(missingPart => !string.IsNullOrEmpty(missingPart))
    .Select(missingPart => scoreCalculator.CalculateScore(missingPart))
    .OrderBy(score => score)
    .ToArrayAsync();

var middleIndex = scoresAscending.Length / 2;

var middleScore = scoresAscending[middleIndex];

Console.WriteLine($"The middle score is {middleScore}.");