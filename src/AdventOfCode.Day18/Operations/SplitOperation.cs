namespace AdventOfCode.Day18.Operations;

internal class SplitOperation
{
    public bool Split(SnailfishNumber number)
    {
        if (number.Left.IsValue && number.Left.Value > 9)
        {
            number.Left = InstantiateSplitValue(number.Left.Value);
            number.Left.Pair.SetParent(number);
            
            return true;
        }

        if (number.Left.IsPair)
        {
            var split = Split(number.Left.Pair);
            
            if (split)
            {
                return true;
            }
        }

        if (number.Right.IsValue && number.Right.Value > 9)
        {
            number.Right = InstantiateSplitValue(number.Right.Value);
            number.Right.Pair.SetParent(number);
            
            return true;
        }

        if (number.Right.IsPair)
        {
            var split = Split(number.Right.Pair);
            
            if (split)
            {
                return true;
            }
        }

        return false;
    }
    
    private static SnailfishValue InstantiateSplitValue(int value) =>
        SnailfishValue.CreatePair(
            new SnailfishNumber(
                SnailfishValue.CreateValue(value / 2), 
                SnailfishValue.CreateValue(value / 2 + value % 2)));
}