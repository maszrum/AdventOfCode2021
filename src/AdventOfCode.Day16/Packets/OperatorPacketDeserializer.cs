namespace AdventOfCode.Day16.Packets;

internal class OperatorPacketDeserializer : IPacketDeserializer
{
    private readonly PacketHeader _header;
    private readonly MessageDeserializer _baseDeserializer;

    public OperatorPacketDeserializer(
        PacketHeader header, 
        MessageDeserializer baseDeserializer)
    {
        _baseDeserializer = baseDeserializer;
        _header = header;
    }

    public ReadOnlySpan<bool> Deserialize(
        ReadOnlySpan<bool> bits, out IPacket packet)
    {
        var lengthTypeId = bits[6];
        
        bits = bits[7..]; // skip header and length type ID
        
        return lengthTypeId
            ? DeserializeWithNumberOfSubPackets(bits, out packet)
            : DeserializeWithTotalLength(bits, out packet);
    }
    
    private ReadOnlySpan<bool> DeserializeWithTotalLength(
        ReadOnlySpan<bool> bits, out IPacket packet)
    {
        var totalLength = bits[..15].ToArray().ToInt32();
        bits = bits[15..];
        
        var subPacketsBits = bits[..totalLength];
        bits = bits[totalLength..];
        
        var subPackets = _baseDeserializer.Deserialize(subPacketsBits);
        
        packet = new OperatorPacket(_header, subPackets);
        
        return bits;
    }
    
    private ReadOnlySpan<bool> DeserializeWithNumberOfSubPackets(
        ReadOnlySpan<bool> bits, out IPacket packet)
    {
        var packetsCount = bits[..11].ToArray().ToInt32();
        bits = bits[11..];
        
        var subPackets = new List<IPacket>(packetsCount);
        
        while (packetsCount > 0)
        {
            bits = _baseDeserializer.DeserializeSingle(bits, out var subPacket);
            
            subPackets.Add(subPacket);
            
            packetsCount--;
        }
        
        packet = new OperatorPacket(_header, subPackets);
        
        return bits;
    }
}