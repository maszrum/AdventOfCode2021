namespace AdventOfCode.Day18;

internal class MagnitudeCalculator
{
    public int Calculate(SnailfishNumber number)
    {
        var result = 0;
        
        checked
        {
            if (number.Left.IsValue)
            {
                result += number.Left.Value * 3;
            }
            else
            {
                result += Calculate(number.Left.Pair) * 3;
            }
        
            if (number.Right.IsValue)
            {
                result += number.Right.Value * 2;
            }
            else
            {
                result += Calculate(number.Right.Pair) * 2;
            }
        }
        
        return result;
    }
}