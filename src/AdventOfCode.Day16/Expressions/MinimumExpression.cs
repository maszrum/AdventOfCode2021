namespace AdventOfCode.Day16.Expressions;

internal class MinimumExpression : IMathExpression
{
    private readonly IReadOnlyList<IMathExpression> _children;

    public MinimumExpression(IReadOnlyList<IMathExpression> children)
    {
        _children = children;
    }

    public long GetValue() => 
        _children.Min(child => child.GetValue());
}