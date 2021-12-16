namespace AdventOfCode.Day16.Expressions;

internal class EqualToExpression : OperatorExpression
{
    public override long GetValue() =>
        Children[0].GetValue() == Children[1].GetValue() 
            ? 1 
            : 0;
}