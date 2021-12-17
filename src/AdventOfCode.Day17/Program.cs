// ReSharper disable file PossibleMultipleEnumeration

var reader = new InputFileReader("input.txt");

var inputLineParts = (await reader.ReadFirstLine())
    .Substring(13)
    .Split(", ");
    
var rangeValues = inputLineParts
    .Select(part => part.Substring(2))
    .SelectMany(part => part.Split(".."))
    .Select(int.Parse)
    .ToArray();

var trench = new OceanTrench(
    minX: rangeValues[0], 
    maxX: rangeValues[1], 
    minY: rangeValues[2], 
    maxY: rangeValues[3]);

var velocities = Enumerable
    .Range(1, trench.MaxX)
    .SelectMany(vX => 
        Enumerable
            .Range(trench.MinY, 500) // max velocity Y is selected experimentally
            .Select(vY => new Vector(vX, vY)));

var simulation = new ShootSimulation(
    startingPosition: Point.Zero, 
    trench: trench);

/*
 * Part one
 */

var highestY = int.MinValue;

foreach (var velocity in velocities)
{
    var highestInShot = int.MinValue;
    var wasHit = false;
    
    simulation.SimulateSteps(
        startingVelocity: velocity, 
        stepFunction: (position, hit) =>
        {
            highestInShot = Math.Max(highestInShot, position.Y);
            
            if (hit)
            {
                wasHit = true;
            }
        });
    
    if (wasHit)
    {
        highestY = Math.Max(highestY, highestInShot);
    }
}

Console.WriteLine($"The probe can reach the maximum Y position of {highestY}.");

/*
 * Part two
 */

var totalHits = 0;

foreach (var velocity in velocities)
{
    simulation.SimulateSteps(
        velocity,
        (_, hit) =>
        {
            if (hit)
            {
                totalHits++;
            }
        });
}

Console.WriteLine($"There are {totalHits} possible starting speeds to reach the target area.");