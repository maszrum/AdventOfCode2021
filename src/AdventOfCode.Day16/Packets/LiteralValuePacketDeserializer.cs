namespace AdventOfCode.Day16.Packets;

internal class LiteralValuePacketDeserializer : IPacketDeserializer
{
    private readonly PacketHeader _header;

    public LiteralValuePacketDeserializer(PacketHeader header)
    {
        _header = header;
    }

    public ReadOnlySpan<bool> Deserialize(
        ReadOnlySpan<bool> bits, out IPacket packet)
    {
        bits = bits[6..]; // skip header
        // var bitsUsed = 6;
        
        var valueBits = new List<bool[]>();

        bool nextExists;
        do
        {
            var bitArray = bits[1..5].ToArray();
            valueBits.Add(bitArray);
            
            nextExists = bits[0];
            
            bits = bits[5..];
            // bitsUsed += 5;
            
            if (!nextExists)
            {
                break;
            }
        }
        while (nextExists);
        
        var value = valueBits
            .SelectMany(bit => bit)
            .ToInt64();
        
        packet = new LiteralValuePacket(_header, value);
        
        // var bytesUsed = (bitsUsed - 1) / 8 + 1;
        // var bitsUnused = bytesUsed * 8 - bitsUsed;
        //
        // return bits[bitsUnused..];
        
        return bits;
    }
}