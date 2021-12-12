/*
 * INTENTIONALLY NON-RECURSIVE SOLUTION
 */

/*
 * Read caves and connections into graph
 */

var reader = new InputFileReader("input.txt");

var cavePairs = await reader.ReadLineByLine()
    .Select(line => line.Split('-'))
    .Select(parts => 
        new
        {
            First = parts[0], 
            Second = parts[1]
        })
    .ToArrayAsync();
    
var caves = cavePairs
    .SelectMany(pair => new[] { pair.First, pair.Second })
    .Distinct()
    .Select(name => new Cave(name))
    .ToArray();

var caveConnections = caves
    .Select(cave => 
        new
        {
            Cave = cave, 
            Connections = cavePairs
                .Where(p => p.First == cave.Name || p.Second == cave.Name)
                .Select(p => p.First == cave.Name ? p.Second : p.First)
                .Select(name => caves.Single(c => c.Name == name))
        })
    .ToDictionary(
        binding => binding.Cave, 
        binding => binding.Connections);
    
Array.ForEach(
    caves, 
    cave => cave.SetupConnections(caveConnections[cave]));

var startCave = caves.Single(c => c.IsStart);

/*
 * Part one
 */

var pathSeekerPartOne = new PathSeeker(new PartOneCavesRestriction());
var foundPathsPartOne = pathSeekerPartOne.Find(startCave);

Console.WriteLine($"Found {foundPathsPartOne.Count} paths in part one.");

/*
 * Part two
 */
 
 
var pathSeekerPartTwo = new PathSeeker(new PartTwoCavesRestriction());
var foundPathsPartTwo = pathSeekerPartTwo.Find(startCave);

Console.WriteLine($"Found {foundPathsPartTwo.Count} paths in part one.");