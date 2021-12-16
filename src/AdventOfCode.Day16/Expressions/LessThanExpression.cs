namespace AdventOfCode.Day16.Expressions;

internal class LessThanExpression : IMathExpression
{
    private readonly IReadOnlyList<IMathExpression> _children;

    public LessThanExpression(IReadOnlyList<IMathExpression> children)
    {
        if (children.Count != 2)
        {
            throw new ArgumentException(
                "must contain 2 elements", nameof(children));
        }
        
        _children = children;
    }

    public long GetValue() =>
        _children[0].GetValue() < _children[1].GetValue() 
            ? 1 
            : 0;
}