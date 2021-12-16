namespace AdventOfCode.Day16.Expressions;

internal class PacketToExpressionConverter
{
    private static readonly Dictionary<byte, Func<OperatorExpression>> ExpressionFactory = 
        new()
        {
            [0] = () => new SumExpression(),
            [1] = () => new ProductExpression(),
            [2] = () => new MinimumExpression(),
            [3] = () => new MaximumExpression(),
            [5] = () => new GreaterThanExpression(),
            [6] = () => new LessThanExpression(),
            [7] = () => new EqualToExpression()
        };

    public IMathExpression Convert(IPacket packet)
    {
        if (packet.Header.TypeId == 4)
        {
            if (packet is not LiteralValuePacket literalValuePacket)
            {
                throw new InvalidOperationException(
                    $"something is wrong with packet, it has invalid type ID");
            }
            
            return new ValueExpression(literalValuePacket.Value);
        }
        
        if (packet is not OperatorPacket operatorPacket)
        {
            throw new InvalidOperationException(
                $"something is wrong with packet, it has invalid type ID");
        }
        
        var operatorExpression = ExpressionFactory[packet.Header.TypeId]();
        
        var children = operatorPacket.SubPackets
            .Select(Convert)
            .ToArray();
        
        operatorExpression.SetupChildren(children);
        
        return operatorExpression;
    }
}