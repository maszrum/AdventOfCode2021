namespace AdventOfCode.Day19;

internal class PointTransformation
{
    private readonly PointTransformation? _previous;
    
    public static PointTransformation NoTransformation => 
        new(new Vector3d(0, 0, 0), p => p);

    public PointTransformation(
        Vector3d translation, 
        Func<Point3d, Point3d> rotation)
    {
        Translation = translation;
        Rotation = rotation;
    }
    
    private PointTransformation(
        Vector3d translation, 
        Func<Point3d, Point3d> rotation, 
        PointTransformation previous) 
        : this(translation, rotation)
    {
        _previous = previous;
    }

    public Vector3d Translation { get; }
    
    public Func<Point3d, Point3d> Rotation { get; }
    
    public Point3d TransformPoint(Point3d point3d)
    {
        if (_previous is not null)
        {
            point3d = _previous.TransformPoint(point3d);
        }
        
        var (rotatedX, rotatedY, rotatedZ) = Rotation(point3d);

        return new Point3d(
            rotatedX + Translation.X,
            rotatedY + Translation.Y,
            rotatedZ + Translation.Z);
    }
    
    public PointTransformation CombineWith(PointTransformation other) => 
        new(other.Translation, other.Rotation, this);
}