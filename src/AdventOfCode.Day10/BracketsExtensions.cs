namespace AdventOfCode.Day10;

internal static class BracketsExtensions
{
    public static bool IsOpeningBracket(this char bracket) => 
        bracket is '{' or '[' or '<' or '(';

    public static bool IsClosingBracket(this char bracket) => 
        bracket is '}' or ']' or '>' or ')';

    public static char ToOpeningBracket(this char openingBracket) =>
        openingBracket switch
        {
            '}' => '{',
            ']' => '[',
            ')' => '(',
            '>' => '<',
            _ => throw new ArgumentOutOfRangeException(
                nameof(openingBracket), openingBracket, null)
        };

    public static char ToClosingBracket(this char openingBracket) =>
        openingBracket switch
        {
            '{' => '}',
            '[' => ']',
            '(' => ')',
            '<' => '>',
            _ => throw new ArgumentOutOfRangeException(
                nameof(openingBracket), openingBracket, null)
        };
}