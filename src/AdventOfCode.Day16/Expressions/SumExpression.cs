namespace AdventOfCode.Day16.Expressions;

internal class SumExpression : IMathExpression
{
    private readonly IReadOnlyList<IMathExpression> _children;

    public SumExpression(IReadOnlyList<IMathExpression> children)
    {
        _children = children;
    }

    public long GetValue() => 
        _children.Sum(child => child.GetValue());
}