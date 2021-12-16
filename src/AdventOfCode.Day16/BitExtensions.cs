namespace AdventOfCode.Day16;

internal static class BitExtensions
{
    public static ReadOnlySpan<byte> ToBytes(this ReadOnlySpan<bool> bits)
    {
        var array = bits.ToArray();
        Array.Reverse(array);
        
        var bitsArray = new BitArray(array);
        
        var arraySize = (bits.Length - 1) / 8 + 1;
        var bytes = new byte[arraySize];
        bitsArray.CopyTo(bytes, 0);
        
        return bytes.AsSpan();
    }
    
    public static byte ToByte(this ReadOnlySpan<bool> bits) => 
        bits.ToBytes()[0];
    
    public static int ToInt(this Span<bool> bits) => 
        bits.ToByte();
    
    public static ReadOnlySpan<byte> ToBytes(this Span<bool> bits) => 
        ((ReadOnlySpan<bool>)bits).ToBytes();

    public static byte ToByte(this Span<bool> bits) => 
        ((ReadOnlySpan<bool>)bits).ToBytes()[0];
    
    public static long ToInt64(this IEnumerable<bool> bits)
    {
        var valueBytes = bits
            .ToArray()
            .AsSpan()
            .ToBytes()
            .ToArray();
        
        var valueBytesLength64 = valueBytes
            .Concat(Enumerable.Repeat((byte)0x00, 8 - valueBytes.Length))
            .ToArray();
        
        return BitConverter.ToInt64(valueBytesLength64);
    }
    
    public static int ToInt32(this IEnumerable<bool> bits)
    {
        var valueBytes = bits
            .ToArray()
            .AsSpan()
            .ToBytes()
            .ToArray();
        
        var valueBytesLength64 = valueBytes
            .Concat(Enumerable.Repeat((byte)0x00, 4 - valueBytes.Length))
            .ToArray();
        
        return BitConverter.ToInt32(valueBytesLength64);
    }
}