namespace AdventOfCode.Day16.Expressions;

internal class MinimumExpression : OperatorExpression
{
    public override long GetValue() => 
        Children.Min(child => child.GetValue());
}