namespace AdventOfCode.Day16.Expressions;

internal class SumExpression : OperatorExpression
{
    public override long GetValue() => 
        Children.Sum(child => child.GetValue());
}