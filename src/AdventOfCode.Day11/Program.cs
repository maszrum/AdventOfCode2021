var reader = new InputFileReader("input.txt");

var matrixBuilder = await reader.ReadLineByLine()
    .Select(line => line.Select(character => (int)char.GetNumericValue(character)))
    .AggregateAsync(
        seed: new RowMatrixBuilder<int>(), 
        accumulator: (builder, row) => builder.AddRow(row));

var matrix = matrixBuilder.BuildMutable();

var group = new GroupOfOctopuses(matrix);

/*
 * Part one
 */

var sumOfFlashes = Enumerable
    .Repeat(group, 100)
    .Select(g =>
    {
        Console.WriteLine(g);
        Console.WriteLine();
        
        return g.Step();
    })
    .Sum();

Console.WriteLine($"There are total of {sumOfFlashes} flashes after 100 steps.");

/*
 * Part two
 */
 
while (!group.IsSynchronizedFlash)
{
    group.Step();
}

Console.WriteLine($"All octopuses flash simultaneously in step {group.StepNumber}.");