namespace AdventOfCode.Day16.Packets;

internal interface IPacket
{
    PacketHeader Header { get; }
}