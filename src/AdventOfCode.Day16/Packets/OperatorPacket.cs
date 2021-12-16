namespace AdventOfCode.Day16.Packets;

internal class OperatorPacket : IPacket
{
    public OperatorPacket(
        PacketHeader header, 
        IReadOnlyList<IPacket> subPackets)
    {
        Header = header;
        SubPackets = subPackets;
    }

    public PacketHeader Header { get; }
    
    public IReadOnlyList<IPacket> SubPackets { get; }
}