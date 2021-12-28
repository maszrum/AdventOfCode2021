/*
 * Part one
 */

var reader = new InputFileReader("input.txt");

var scannersAndPoints = await reader.ReadLineByLine()
    .Where(line => !string.IsNullOrEmpty(line))
    .ReadScannedPoints();

var potentialMatchedScanners = new PointsSetsMatcher(scannersAndPoints)
    .Match()
    .ToArray();

var transformationSeeker = new TransformationSeeker();

var possibleTransformations = potentialMatchedScanners
    .Concat(potentialMatchedScanners.Select(MatchedScanners.Reverse))
    .SelectMany(match => 
        transformationSeeker.Find(
            originSystem: scannersAndPoints[match.ScannerA], 
            transformedSystem: scannersAndPoints[match.ScannerB])
            .Select(transformation => (Transformation: transformation, Match: match)));

var transformations = possibleTransformations
    .Where(t =>
    {
        var (transformation, match) = t;
        
        var pointsTransformedToOriginSystem = scannersAndPoints[match.ScannerB]
            .Select(transformation.TransformPoint);
        
        var commonPoints = pointsTransformedToOriginSystem
            .Intersect(scannersAndPoints[match.ScannerA])
            .ToArray();
        
        return commonPoints.Length >= 12;
    })
    .ToDictionary(
        t => MatchedScanners.Reverse(t.Match), 
        t => t.Transformation);

var matchedScanners = transformations.Keys.ToArray();

var possibleRoutes = scannersAndPoints.Keys
    .Select(scanner => new PossibleRoute(scanner))
    .ToArray();

Array.ForEach(possibleRoutes, possibleRoute =>
{
    var scanner = possibleRoute.From;
        
    var others = matchedScanners
        .Where(m => m.ScannerB == scanner)
        .Select(m => m.ScannerA)
        .Select(s => possibleRoutes.Single(pr => pr.From == s))
        .ToArray();
    
    possibleRoute.SetupRoutes(others);
});

var rootNode = possibleRoutes
    .Single(r => r.From == new Scanner(0));

var routeSeeker = new RouteSeeker(destination: rootNode.From);

var toOriginTransformations = scannersAndPoints.Keys
    .Where(scanner => rootNode.From != scanner)
    .ToDictionary(
        scanner => scanner,
        scanner => routeSeeker
            .Find(possibleRoutes.Single(r => r.From == scanner))
            .Select(match => transformations[match])
            .Aggregate(
                seed: PointTransformation.NoTransformation,
                func: (combined, current) => combined.CombineWith(current)));

toOriginTransformations.Add(rootNode.From, PointTransformation.NoTransformation);

var realScannerPositions = scannersAndPoints.Keys
    .ToDictionary(
        scanner => scanner,
        scanner => toOriginTransformations[scanner].TransformPoint(new Point3d(0, 0, 0)));

var realBeaconPositions = scannersAndPoints
    .SelectMany(kvp => 
        kvp.Value
            .Select(point => toOriginTransformations[kvp.Key].TransformPoint(point)))
    .Distinct()
    .ToArray();

var beaconsCount = realBeaconPositions.Length;

Console.WriteLine($"Total number of beacons is {beaconsCount}.");

/*
 * Part two
 */

var maxDistance = new Permutator()
    .WithoutRepetitions(realScannerPositions.Values, 2)
    .Select(points => points.ToArray())
    .Max(points => new ManhattanDistanceCalculator().Calculate(points[0], points[1]));

Console.WriteLine($"The largest Manhattan distance between two points is {maxDistance}.");