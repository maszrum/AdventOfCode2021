namespace AdventOfCode.Day10;

internal class AutocompletionScoreCalculator
{
    private static readonly IReadOnlyDictionary<char, int> Points = new Dictionary<char, int>
    {
        [')'] = 1,
        [']'] = 2,
        ['}'] = 3,
        ['>'] = 4
    };

    public long CalculateScore(string input)
    {
        var totalScore = 0L;
        
        checked
        {
            foreach (var bracket in input)
            {
                totalScore *= 5;
                totalScore += Points[bracket];
            }
        }
        
        return totalScore;
    }
}