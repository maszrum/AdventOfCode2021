namespace AdventOfCode.Day22;

internal readonly struct Cuboid
{
    public Cuboid(
        LongPoint3d lowCorner, LongPoint3d highCorner)
    {
        LowCorner = lowCorner;
        HighCorner = highCorner;
    }

    public LongPoint3d LowCorner { get; }
    
    public LongPoint3d HighCorner { get; }
    
    public long CountCubes()
    {
        checked
        {
            var x = HighCorner.X - LowCorner.X + 1;
            var y = HighCorner.Y - LowCorner.Y + 1;
            var z = HighCorner.Z - LowCorner.Z + 1;
        
            return x * y * z;
        }
    }
    
    public bool DoesOverlapWith(Cuboid other)
    {
        static bool HaveCommonPoints(MinMax a, MinMax b) => 
            a.Max >= b.Min && b.Max >= a.Min;
        
        var componentFunctions = new Func<LongPoint3d, long>[]
        {
            p => p.X,
            p => p.Y,
            p => p.Z
        };
        
        var lowCorner = LowCorner;
        var highCorner = HighCorner;
        
        return componentFunctions
            .Select(func =>
            {
                var low = func(lowCorner);
                var high = func(highCorner);
                var otherLow = func(other.LowCorner);
                var otherHigh = func(other.HighCorner);
                
                var a = MinMax.Create(low, high);
                var b = MinMax.Create(otherLow, otherHigh);
                
                return HaveCommonPoints(a, b);
            })
            .All(b => b);
    }

    public IEnumerable<Cuboid> Split(Cuboid other)
    {
        if (!DoesOverlapWith(other))
        {
            yield return this;
            yield break;
        }
        
        var left = Math.Max(LowCorner.X, other.LowCorner.X);
        var right = Math.Min(HighCorner.X, other.HighCorner.X);
        var bottom = Math.Max(LowCorner.Y, other.LowCorner.Y);
        var top = Math.Min(HighCorner.Y,  other.HighCorner.Y);

        if (other.LowCorner.X > LowCorner.X)
        {
            yield return new Cuboid(
                new LongPoint3d(LowCorner.X, LowCorner.Y, LowCorner.Z),
                new LongPoint3d(other.LowCorner.X - 1, HighCorner.Y, HighCorner.Z));
        }

        if (other.HighCorner.X < HighCorner.X)
        {
            yield return new Cuboid(
                new LongPoint3d(other.HighCorner.X + 1, LowCorner.Y, LowCorner.Z),  
                new LongPoint3d(HighCorner.X, HighCorner.Y, HighCorner.Z));
        }

        if (other.LowCorner.Y > LowCorner.Y)
        {
            yield return new Cuboid(
                new LongPoint3d(left, LowCorner.Y, LowCorner.Z), 
                new LongPoint3d(right, other.LowCorner.Y - 1, HighCorner.Z));
        }

        if (other.HighCorner.Y < HighCorner.Y)
        {
            yield return new Cuboid(
                new LongPoint3d(left, other.HighCorner.Y + 1, LowCorner.Z), 
                new LongPoint3d(right, HighCorner.Y, HighCorner.Z));
        }

        if (other.LowCorner.Z > LowCorner.Z)
        {
            yield return new Cuboid(
                new LongPoint3d(left, bottom, LowCorner.Z),
                new LongPoint3d(right, top, other.LowCorner.Z - 1));
        }

        if (other.HighCorner.Z < HighCorner.Z)
        {
            yield return new Cuboid(
                new LongPoint3d(left, bottom, other.HighCorner.Z + 1),
                new LongPoint3d(right, top, HighCorner.Z));
        }
    }

    public override string ToString() => $"{LowCorner}, {HighCorner}";
}