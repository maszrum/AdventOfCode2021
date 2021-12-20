/*
 * Read enhancement algorithm into string
 */

var reader = new InputFileReader("input.txt");

var algorithmData = (await reader.ReadFirstLine())
    .Select(character => character == '#')
    .ToArray();

/*
 * Read image points into hash set
 */

var inputImagePoints = (await reader.ReadLineByLine()
    .Skip(2)
    .AggregateAsync(
        seed: new { LitPoints = ImmutableHashSet<Point>.Empty, Y = 0 }, 
        accumulator: (state, line) =>
        {
            var points = line
                .Select((character, x) => 
                    character == '#' 
                        ? new Point(x, state.Y) 
                        : Point.Invalid)
                .Where(Point.IsValid);
            
            var litPoints = state.LitPoints.Union(points);

            return new
            {
                LitPoints = litPoints, 
                Y = state.Y + 1
            };
        }))
    .LitPoints;

var enhancer = new ImageEnhancer(algorithmData, inputImagePoints);

/*
 * Write images to console?
 */

var printer = new ImagePrinter();
//void Print(string line) => Console.WriteLine(line);
void Print(string line) { /* ignore */ };

/*
 * Part one
 */

Print("Input image:");
printer.Print(inputImagePoints, Print);

for (var i = 1; i <= 2; i++)
{
    enhancer.Enhance();
    
    Print($"After #{enhancer.Step} enhance:");
    printer.Print(enhancer.CurrentImage, Print);
}

Console.WriteLine(
    $"There are {enhancer.CurrentImage.Count} pixels after {enhancer.Step} enhancement processes.");

/*
 * Part two
 */

for (var i = 3; i <= 50; i++)
{
    enhancer.Enhance();
    
    Print($"After #{enhancer.Step} enhance:");
    printer.Print(enhancer.CurrentImage, Print);
}

Console.WriteLine(
    $"There are {enhancer.CurrentImage.Count} pixels after {enhancer.Step} enhancement processes.");