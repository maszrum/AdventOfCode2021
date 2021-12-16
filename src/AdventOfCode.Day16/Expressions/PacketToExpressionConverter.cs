namespace AdventOfCode.Day16.Expressions;

internal class PacketToExpressionConverter
{
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
        
        var operatorExpression = InstantiateOperatorExpression(
            typeId: packet.Header.TypeId, 
            children: children);
        
        return operatorExpression;
    }
    
    private static IMathExpression InstantiateOperatorExpression(
        byte typeId, IReadOnlyList<IMathExpression> children)
    {
        return typeId switch
        {
            0 => new SumExpression(children),
            1 => new ProductExpression(children),
            2 => new MinimumExpression(children),
            3 => new MaximumExpression(children),
            5 => new GreaterThanExpression(children),
            6 => new LessThanExpression(children),
            7 => new EqualToExpression(children),
            _ => throw new ArgumentOutOfRangeException(nameof(typeId), typeId, "invalid value")
        };
    }
}