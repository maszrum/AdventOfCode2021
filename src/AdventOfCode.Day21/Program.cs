/*
 * Part one
 */

var reader = new InputFileReader("input.txt");

var startingPositions = (await reader.ReadAllLines())
    .Select(line => line[28..])
    .Select(int.Parse)
    .ToArray();

var dice = new DeterministicDice();
var game = new DiracDiceGame(dice, startingPositions[0], startingPositions[1]);

while (!game.IsEnded)
{
    game.Turn();
}

var partOneResult = dice.TotalRolls * game.GetLooser().Points;

Console.WriteLine($"The result of the part one is {partOneResult}.");

/*
 * Part two
 */

/*
 * #3 - 1 combination
 * #4 - 3 combinations
 * #5 - 6 combinations
 * #6 - 7 combinations
 * #7 - 6 combinations
 * #8 - 3 combinations
 * #9 - 1 combination
 */
 
Console.WriteLine("no solution");