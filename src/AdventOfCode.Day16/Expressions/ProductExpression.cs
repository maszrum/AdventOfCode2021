namespace AdventOfCode.Day16.Expressions;

internal class ProductExpression : IMathExpression
{
    private readonly IReadOnlyList<IMathExpression> _children;

    public ProductExpression(IReadOnlyList<IMathExpression> children)
    {
        _children = children;
    }

    public long GetValue() =>
        _children.Aggregate(
            seed: 1L, 
            func: (product, child) => product * child.GetValue());
}
