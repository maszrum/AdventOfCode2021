var reader = new InputFileReader("input.txt");

async Task<int> CountPointsWithAtLeastTwoOverlaps(IAsyncEnumerable<Line> lines)
{
    var ventsCount = new Dictionary<Point, int>();

    await foreach (var line in lines)
    {
        foreach (var point in line.GetPointsBetween())
        {
            if (ventsCount.ContainsKey(point))
            {
                ventsCount[point]++;
            }
            else
            {
                ventsCount.Add(point, 1);
            }
        }
    }

    return ventsCount.Count(kvp => kvp.Value >= 2);
}

/*
 * Part one
 */

var horizontalAndVerticalLines = reader.ReadLineByLine()
    .ParseLines()
    .Where(line => line.IsHorizontal || line.IsVertical);

var pointsPartOne = await CountPointsWithAtLeastTwoOverlaps(horizontalAndVerticalLines);

Console.WriteLine(
    $"The number of points where at least two lines overlap (horizontal, vertical): {pointsPartOne}");

/*
 * Part two
 */

var horizontalVerticalAndDiagonalLines = reader.ReadLineByLine()
    .ParseLines()
    .Where(line => line.IsHorizontal || line.IsVertical || line.IsDiagonal);

var pointsPartTwo = await CountPointsWithAtLeastTwoOverlaps(horizontalVerticalAndDiagonalLines);

Console.WriteLine(
    $"The number of points where at least two lines overlap (horizontal, vertical, diagonal): {pointsPartTwo}");