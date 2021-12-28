var reader = new InputFileReader("input.txt");

var steps = await reader
    .ReadLineByLine()
    .ReadSteps()
    .ToArrayAsync();

var shapes = Enumerable.Empty<Cuboid>();

foreach (var step in steps)
{
    shapes = shapes.SelectMany(s => s.Split(step.Cubes));

    if (step.OnOff)
    {
        shapes = shapes.Append(step.Cubes);
    }
}

var finalShapes = shapes.ToArray();

/*
 * Part one
 */

var partOneArea = new Cuboid(
    new LongPoint3d(-50, -50, -50), 
    new LongPoint3d(50, 50, 50));

var partOneCubesCount = finalShapes
    .Where(c => c.DoesOverlapWith(partOneArea))
    .Sum(c => c.CountCubes());

Console.WriteLine($"There are {partOneCubesCount} cubes on (part one).");

/*
 * Part two
 */

var partTwoCubesCount = finalShapes
    .Sum(c => c.CountCubes());

Console.WriteLine($"There are {partTwoCubesCount} cubes on (part two).");