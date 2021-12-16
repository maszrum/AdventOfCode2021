namespace AdventOfCode.Day16.Expressions;

internal abstract class OperatorExpression : IMathExpression
{
    private IReadOnlyList<IMathExpression>? _children;
    protected IReadOnlyList<IMathExpression> Children => 
        _children ?? throw new InvalidOperationException($"call {nameof(SetupChildren)} before");

    public void SetupChildren(IReadOnlyList<IMathExpression> expressions)
    {
        _children = expressions;
    }

    public abstract long GetValue();
}