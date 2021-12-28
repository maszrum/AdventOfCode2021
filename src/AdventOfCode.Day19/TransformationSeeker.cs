namespace AdventOfCode.Day19;

internal class TransformationSeeker
{
    private readonly Permutator _permutator = new();
    
    public IEnumerable<PointTransformation> Find(HashSet<Point3d> originSystem, HashSet<Point3d> transformedSystem)
    {
        var originDistances = CalculateDistanceBetweenEach(originSystem);
        var transformedDistances = CalculateDistanceBetweenEach(transformedSystem).ToArray();
    
        var matchingOriginLines = originDistances
            .Where(a => 
                transformedDistances.Count(b => DoublesEqual(b.Distance, a.Distance)) >= 1);

        var transformations = new List<(PointTransformation Transformation, int TransformationIndex)>();
        
        foreach (var matchingOriginLine in matchingOriginLines)
        {
            var matchingTransformedLine = transformedDistances
                .Single(b => DoublesEqual(b.Distance, matchingOriginLine.Distance));
    
            var rotateTransformations = GetRotationTransformations();
        
            var foundRotateTransformation = rotateTransformations
                .Select(
                    (rotateTransformation, index) => new
                    {
                        TransformationIndex = index,
                        Rotation = rotateTransformation,
                        A = rotateTransformation(matchingTransformedLine.A),
                        B = rotateTransformation(matchingTransformedLine.B)
                    })
                .FirstOrDefault(
                    transformed =>
                    {
                        var fromAtoA = matchingOriginLine.A - transformed.A;
                        var fromBtoB = matchingOriginLine.B - transformed.B;

                        return fromAtoA == fromBtoB;
                    });
        
            if (foundRotateTransformation is null)
            {
                continue;
            }
            
            var originPoint = matchingOriginLine.A;
            var transformedPoint = foundRotateTransformation.Rotation(matchingTransformedLine.A);
            
            var transformation = new PointTransformation(
                translation: originPoint - transformedPoint, 
                rotation: foundRotateTransformation.Rotation);
            
            transformations.Add((transformation, foundRotateTransformation.TransformationIndex));
        }
        
        return transformations
            .DistinctBy(t => t.TransformationIndex)
            .Select(t => t.Transformation);
    }
    
    private IEnumerable<(Point3d A, Point3d B, double Distance)> CalculateDistanceBetweenEach(
        IEnumerable<Point3d> points)
    {
        return _permutator
            .WithoutRepetitions(points, 2)
            .Select(pair => pair.ToArray())
            .Select(pair => 
                (A: pair[0], B: pair[1], Distance: pair[0].DistanceTo(pair[1])));
    }
    
    private static bool DoublesEqual(double a, double b) => 
        Math.Abs(a - b) < 0.00001;
    
    private IEnumerable<Func<Point3d, Point3d>> GetRotationTransformations()
    {
        var rotateTransformations = _permutator
            .WithRepetitions(
                items: new[] { 1, -1 }, 
                count: 3)
            .Select(m => m.ToArray())
            .Select(m => new Func<Point3d, Point3d>(point => new Point3d(point.X * m[0], point.Y * m[1], point.Z * m[2])))
            .ToArray();
    
        var axisTransformations = new Func<Point3d, Point3d>[]
        {
            point => point,
            point => new Point3d(point.X, point.Z, point.Y),
            point => new Point3d(point.Y, point.Z, point.X),
            point => new Point3d(point.Y, point.X, point.Z),
            point => new Point3d(point.Z, point.X, point.Y),
            point => new Point3d(point.Z, point.Y, point.X)
        };
    
        return Enumerable
            .Range(0, rotateTransformations.Length)
            .SelectMany(
                rt => Enumerable
                    .Range(0, axisTransformations.Length)
                    .Select(at => new { RotateIndex = rt, AxisIndex = at }))
            .Select(indexes =>
            {
                var rotate = rotateTransformations[indexes.RotateIndex];
                var axis = axisTransformations[indexes.AxisIndex];
            
                return new Func<Point3d, Point3d>(point => rotate(axis(point)));
            });
    }
}