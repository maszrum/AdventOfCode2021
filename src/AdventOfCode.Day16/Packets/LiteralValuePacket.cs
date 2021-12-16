namespace AdventOfCode.Day16.Packets;

internal class LiteralValuePacket : IPacket
{
    public LiteralValuePacket(PacketHeader header, long value)
    {
        Header = header;
        Value = value;
    }

    public PacketHeader Header { get; }
    
    public long Value { get; }
}