using AdventOfCode.Day18.Operations;

namespace AdventOfCode.Day18;

internal class SnailfishNumber
{
    public SnailfishNumber(
        SnailfishValue left, 
        SnailfishValue right)
    {
        Left = left;
        Right = right;
    }

    public SnailfishValue Left { get; set; }
    
    public SnailfishValue Right { get; set; }
    
    private SnailfishNumber? _parent;
    public SnailfishNumber Parent => 
        _parent ?? throw new InvalidOperationException("number is root");
    
    public bool IsRoot => _parent is null;
    
    public void SetParent(SnailfishNumber parent)
    {
        _parent = parent;
    }
    
    public void RemoveParent()
    {
        _parent = null;
    }
    
    public SnailfishNumber Clone()
    {
        var left = Left.IsPair
            ? SnailfishValue.CreatePair(Left.Pair.Clone())
            : SnailfishValue.CreateValue(Left.Value);
        
        var right = Right.IsPair
            ? SnailfishValue.CreatePair(Right.Pair.Clone())
            : SnailfishValue.CreateValue(Right.Value);
        
        var number = new SnailfishNumber(left, right);
        
        if (left.IsPair)
        {
            left.Pair.SetParent(number);
        }
        if (right.IsPair)
        {
            right.Pair.SetParent(number);
        }
        
        return number;
    }

    public override string ToString() => 
        $"[{Left},{Right}]";

    public static SnailfishNumber operator +(SnailfishNumber a, SnailfishNumber b) => 
        new SumOperation().Do(a, b);
}