namespace AdventOfCode.Day16.Expressions;

internal class ProductExpression : OperatorExpression
{
    public override long GetValue() =>
        Children.Aggregate(
            seed: 1L, 
            func: (product, child) => product * child.GetValue());
}