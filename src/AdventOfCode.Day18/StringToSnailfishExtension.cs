namespace AdventOfCode.Day18;

internal static class StringToSnailfishExtension
{
    public static SnailfishNumber ToSnailfishNumber(this string input) =>
        input
            .AsSpan()
            .ToSnailfishNumber();

    private static SnailfishNumber ToSnailfishNumber(this ReadOnlySpan<char> input)
    {
        input = input[1..^1]; // remove brackets
        
        var commaIndex = FindCommaIndex(input);
        
        var firstPart = input[..commaIndex];
        var secondPart = input[(commaIndex + 1)..];

        var first = firstPart.Length == 1
            ? SnailfishValue.CreateValue(firstPart[0].ToValue())
            : SnailfishValue.CreatePair(ToSnailfishNumber(firstPart));
        
        var second = secondPart.Length == 1
            ? SnailfishValue.CreateValue(secondPart[0].ToValue()) 
            : SnailfishValue.CreatePair(ToSnailfishNumber(secondPart));
        
        var number = new SnailfishNumber(first, second);
        
        if (first.IsPair)
        {
            first.Pair.SetParent(number);
        }
        
        if (second.IsPair)
        {
            second.Pair.SetParent(number);
        }
        
        return number;
    }
    
    private static int FindCommaIndex(ReadOnlySpan<char> input)
    {
        var commaIndex = -1;
        var bracketCounter = 0;
        
        for (var i = 0; i < input.Length; i++)
        {
            var c = input[i];
            
            if (c == '[')
            {
                bracketCounter++;
            }
            else if (c == ']')
            {
                bracketCounter--;
            }
            
            if (bracketCounter == 0 && c == ',')
            {
                commaIndex = i;
                break;
            }
        }
        
        return commaIndex;
    }
    
    private static int ToValue(this char c) => c - '0';
}