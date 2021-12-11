var reader = new InputFileReader("input.txt");

var matrixBuilder = await reader.ReadLineByLine()
    .Select(line => line.Select(character => (int)char.GetNumericValue(character)))
    .AggregateAsync(
        seed: new MatrixBuilder<int>(), 
        accumulator: (builder, row) => builder.AddRow(row));

var matrix = matrixBuilder.Build();

/*
 * Part one
 */

var minima = matrix
    .SelectMany((row, y) =>
    {
        var localMinima = new LocalMinimaSeeker().FindMinima(row);
        return localMinima.Select(x => new PointWithValue<int>(x, y, row[x]));
    })
    .Where(point =>
    {
        var hasTop = matrix.TryGetTopValue(point, out var topValue);
        var hasBottom = matrix.TryGetBottomValue(point, out var bottomValue);
        return (!hasTop || topValue > point.Value) && (!hasBottom || bottomValue > point.Value);
    })
    .ToArray();

var sumOfRiskLevels = minima.Sum(point => 1 + point.Value);

Console.WriteLine($"The sum of risks levels is {sumOfRiskLevels}");

/*
 * Part two
 */
 
var basinSeeker = new BasinsSeeker(matrix);
var basins = basinSeeker.Find(minima);

var top3BasinSizes = basins
    .Select(basin => basin.Count)
    .OrderByDescending(count => count)
    .Take(3)
    .ToArray();

var productOfBiggestBasins = top3BasinSizes[0] * top3BasinSizes[1] * top3BasinSizes[2];

Console.WriteLine($"The product of size of the three largest basins is {productOfBiggestBasins}.");