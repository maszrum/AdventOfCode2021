namespace AdventOfCode.Day16.Packets;

internal class PacketHeader
{
    public PacketHeader(byte version, byte typeId)
    {
        Version = version;
        TypeId = typeId;
    }

    public byte Version { get; }
    
    public byte TypeId { get; }
}