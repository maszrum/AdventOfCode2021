/*
 * Read point coordinates
 */

var reader = new InputFileReader("input.txt");

var inputPoints = await reader.ReadLineByLine()
    .TakeWhile(line => !line.StartsWith("fold"))
    .Where(line => !string.IsNullOrEmpty(line))
    .Select(line => line.Split(','))
    .Select(parts => 
        new Point(
            X: int.Parse(parts[0]), 
            Y: int.Parse(parts[1])))
    .ToArrayAsync();

var foldingInstructionsFactory = new Dictionary<string, Func<int, IFoldingInstruction>>
{
    ["x"] = x => new FoldAlongX(x),
    ["y"] = y => new FoldAlongY(y)
};

/*
 * Read folding instructions
 */

var foldingInstructions = await reader.ReadLineByLine()
    .SkipWhile(line => !line.StartsWith("fold"))
    .Where(line => line.StartsWith("fold along "))
    .Select(line => line[11..].Split('='))
    .Select(parts =>
    {
        var factory = foldingInstructionsFactory[parts[0]];
        return factory(int.Parse(parts[1]));
    })
    .ToArrayAsync();

/*
 * Part one
 */

var dotsVisible = foldingInstructions[0].Fold(inputPoints).Count;

Console.WriteLine($"There will be {dotsVisible} dots visible after the first fold instruction.");

/*
 * Part two
 */

var resultPoints = foldingInstructions
    .Aggregate(
        (IReadOnlyList<Point>)inputPoints, 
        (points, instruction) => instruction.Fold(points))
    .Select(point => point.WithValue(true));

var matrix = new CoordinateMatrixBuilder<bool>()
    .AddPoints(resultPoints)
    .Build();

Console.WriteLine();
Console.WriteLine("Code is:");

var lines = matrix.ToString(value => value ? "X" : " ");
foreach (var line in lines)
{
    Console.WriteLine(line);
}