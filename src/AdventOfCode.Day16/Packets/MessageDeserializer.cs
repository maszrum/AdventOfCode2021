namespace AdventOfCode.Day16.Packets;

internal class MessageDeserializer
{
    public IReadOnlyList<IPacket> Deserialize(ReadOnlySpan<bool> bits)
    {
        var packets = new List<IPacket>();
        
        while (bits.Length > 7)
        {
            bits = DeserializeSingle(bits, out var packet);
            
            packets.Add(packet);
        }
        
        return packets;
    }
    
    public ReadOnlySpan<bool> DeserializeSingle(ReadOnlySpan<bool> bits, out IPacket packet)
    {
        var header = DeserializeHeader(bits[..6]);
        
        var deserializer = InstantiateDeserializer(header);
        
        return deserializer.Deserialize(bits, out packet);
    }

    private static PacketHeader DeserializeHeader(ReadOnlySpan<bool> bits)
    {
        var version = bits[..3].ToByte();
        var typeId = bits[3..6].ToByte();
        
        return new PacketHeader(version, typeId);
    }
    
    private IPacketDeserializer InstantiateDeserializer(PacketHeader header) =>
        header.TypeId == 4 
            ? new LiteralValuePacketDeserializer(header) 
            : new OperatorPacketDeserializer(header, this);
}