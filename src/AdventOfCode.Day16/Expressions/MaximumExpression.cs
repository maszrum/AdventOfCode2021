namespace AdventOfCode.Day16.Expressions;

internal class MaximumExpression : IMathExpression
{
    private readonly IReadOnlyList<IMathExpression> _children;

    public MaximumExpression(IReadOnlyList<IMathExpression> children)
    {
        _children = children;
    }

    public long GetValue() => 
        _children.Max(child => child.GetValue());
}