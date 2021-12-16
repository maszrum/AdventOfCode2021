/*
 * Read input into bool array
 */

var reader = new InputFileReader("input.txt");

var inputText = await reader.ReadFirstLine();

var inputBytes = Convert
    .FromHexString(inputText)
    .Reverse()
    .ToArray();

var bitArray = new BitArray(inputBytes);

var inputBits = new bool[bitArray.Length];
bitArray.CopyTo(inputBits, 0);
Array.Reverse(inputBits);

/*
 * Deserialize data to packets
 */

var deserializer = new MessageDeserializer();
var rootPacket = deserializer.Deserialize(inputBits.AsSpan())[0];

/*
 * Part one
 */

int SumPacketVersions(IPacket packet)
{
    if (packet is OperatorPacket operatorPacket)
    {
        return packet.Header.Version + 
               operatorPacket.SubPackets.Sum(SumPacketVersions);
    }
    
    return packet.Header.Version;
}

var sumOfPacketVersions = SumPacketVersions(rootPacket);

Console.WriteLine($"Sum of the packet versions is equal to {sumOfPacketVersions}");

/*
 * Part two
 */

var converter = new PacketToExpressionConverter();
var rootExpression = converter.Convert(rootPacket);

var expressionValue = rootExpression.GetValue();

Console.WriteLine($"Evaluated expression returned value {expressionValue}");