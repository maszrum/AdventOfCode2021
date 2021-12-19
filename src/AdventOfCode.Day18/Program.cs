/*
 * Read input file into array of SnailfishNumber objects.
 */

var reader = new InputFileReader("input.txt");

var snailfishNumbers = await reader.ReadLineByLine()
    .Select(line => line.ToSnailfishNumber())
    .ToArrayAsync();

var magnitudeCalculator = new MagnitudeCalculator();

/*
 * Part one
 */

var sum = snailfishNumbers
    .Skip(1)
    .Aggregate(
        snailfishNumbers.First(), 
        (sum, number) => sum + number);

var finalSumMagnitude = magnitudeCalculator.Calculate(sum);

Console.WriteLine($"The magnitude of the final sum is {finalSumMagnitude}.");

/*
 * Part two
 */

var largestMagnitude = Enumerable.Range(0, snailfishNumbers.Length)
    .SelectMany(x => Enumerable
        .Range(0, snailfishNumbers.Length)
        .Select(y => x == y 
            ? int.MinValue 
            : magnitudeCalculator.Calculate(snailfishNumbers[x] + snailfishNumbers[y])))
    .Max();

Console.WriteLine($"The largest magnitude of any sum is {largestMagnitude}.");