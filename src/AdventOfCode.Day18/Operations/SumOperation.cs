namespace AdventOfCode.Day18.Operations;

internal class SumOperation
{
    private readonly ExplodeOperation _explodeOperation = new();
    private readonly SplitOperation _splitOperation = new();
    
    public SnailfishNumber Do(SnailfishNumber left, SnailfishNumber right)
    {
        var leftCloned = left.Clone();
        var rightCloned = right.Clone();
        
        var result = new SnailfishNumber(
            SnailfishValue.CreatePair(leftCloned),
            SnailfishValue.CreatePair(rightCloned));
        
        leftCloned.SetParent(result);
        rightCloned.SetParent(result);
        
        bool exploded;
        var split = false;
        
        do
        {
            exploded = _explodeOperation.Do(result);
            
            if (!exploded)
            {
                split = _splitOperation.Split(result);
            }
        }
        while (exploded || split);
        
        return result;
    }
}