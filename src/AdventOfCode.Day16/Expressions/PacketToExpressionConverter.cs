namespace AdventOfCode.Day16.Expressions;

internal class PacketToExpressionConverter
{
    private static readonly Dictionary<byte, Func<IReadOnlyList<IMathExpression>, IMathExpression>> ExpressionFactory = 
        new()
        {
            [0] = children => new SumExpression(children),
            [1] = children => new ProductExpression(children),
            [2] = children => new MinimumExpression(children),
            [3] = children => new MaximumExpression(children),
            [5] = children => new GreaterThanExpression(children),
            [6] = children => new LessThanExpression(children),
            [7] = children => new EqualToExpression(children)
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
        
        var children = operatorPacket.SubPackets
            .Select(Convert)
            .ToArray();
        
        var operatorExpression = ExpressionFactory[packet.Header.TypeId](children);
        
        return operatorExpression;
    }
}