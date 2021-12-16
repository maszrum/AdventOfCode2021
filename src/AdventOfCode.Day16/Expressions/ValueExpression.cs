namespace AdventOfCode.Day16.Expressions;

internal class ValueExpression : IMathExpression
{
    private readonly long _value;

    public ValueExpression(long value)
    {
        _value = value;
    }

    public long GetValue() => _value;
}