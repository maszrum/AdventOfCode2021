namespace AdventOfCode.Day22;

internal readonly struct RebootStep
{
    public RebootStep(bool onOff, Cuboid cubes)
    {
        OnOff = onOff;
        Cubes = cubes;
    }

    public bool OnOff { get; }
    
    public Cuboid Cubes { get; }

    public override string ToString()
    {
        var onOff = OnOff ? "on" : "off";
        return $"{onOff}, {Cubes}";
    }
}