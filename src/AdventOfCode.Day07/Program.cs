var reader = new InputFileReader("input.txt");

var crabPositions = (await reader.ReadFirstLine())
    .Split(',')
    .Select(int.Parse)
    .ToArray();

/*
 * Part one
 */

var solverOne = new MinimalCostSolver(
    costFunction: (from, to) => Math.Abs(from - to));

var fuelPartOne = solverOne.FindMinimalCost(crabPositions);

Console.WriteLine($"The crabs must spend {fuelPartOne} fuel.");

/*
 * Part two
 */

int SumOfArithmeticSequence(int n) => (n + n * n) / 2;

var solverTwo = new MinimalCostSolver(
    costFunction: (from, to) => SumOfArithmeticSequence(Math.Abs(from - to)));

var fuelPartTwo = solverTwo.FindMinimalCost(crabPositions);

Console.WriteLine($"The crabs must spend {fuelPartTwo} fuel.");