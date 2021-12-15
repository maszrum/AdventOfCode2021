/*
 * Read input to matrix
 */

var reader = new InputFileReader("input.txt");

var matrixBuilder = await reader.ReadLineByLine()
    .Select(line => 
        line.Select(character => int.Parse(character.ToString())))
    .AggregateAsync(
        seed: new RowMatrixBuilder<int>(), 
        accumulator: (builder, row) => builder.AddRow(row));

var map = matrixBuilder.Build();

/*
 * Part one
 */

var seekerPartOne = new MinimalTotalRiskSeeker(map);

var totalRiskPartOne = seekerPartOne.Find(
    start: new Point(0, 0), 
    end: new Point(map.Columns - 1, map.Rows - 1));

Console.WriteLine($"Lowest total risk is {totalRiskPartOne}");

/*
 * Part two
 */

var extender = new MapExtender(map);
var extendedMap = extender.Extend(5);

var seekerPartTwo = new MinimalTotalRiskSeeker(extendedMap);

var totalRiskPartTwo = seekerPartTwo.Find(
    start: new Point(0, 0), 
    end: new Point(extendedMap.Columns - 1, extendedMap.Rows - 1));

Console.WriteLine($"Lowest total risk on extended map is {totalRiskPartTwo}");