namespace AdventOfCode.Day16.Expressions;

internal class MaximumExpression : OperatorExpression
{
    public override long GetValue() => 
        Children.Max(child => child.GetValue());
}