namespace AdventOfCode.Day16.Packets;

internal interface IPacketDeserializer
{
    ReadOnlySpan<bool> Deserialize(ReadOnlySpan<bool> bits, out IPacket packet);
}