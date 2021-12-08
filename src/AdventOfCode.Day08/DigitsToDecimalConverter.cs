namespace AdventOfCode.Day08;

internal class DigitsToDecimalConverter
{
    public int Convert(IReadOnlyList<int> digits)
    {
        var result = 0;
        
        for (var i = 0; i < digits.Count; i++)
        {
            var exponent = digits.Count - i - 1;
            result += digits[i] * Power(10, exponent);
        }
        
        return result;
    }
    
    private static int Power(int x, int y)
    {
        var result = 1;
        while (y != 0)
        {
            if ((y & 1) == 1)
            {
                result *= x;
            }
            x *= x;
            y >>= 1;
        }
        return result;
    }
}