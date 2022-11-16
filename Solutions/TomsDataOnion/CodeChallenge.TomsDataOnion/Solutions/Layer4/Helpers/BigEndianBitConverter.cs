namespace CodeChallenge.TomsDataOnion.Solutions.Layer4.Helpers;

public class BigEndianBitConverter
{
    public static ushort ToUInt16(ReadOnlySpan<byte> bytes)
    {
        return BitConverter.ToUInt16(BitConverter.IsLittleEndian
            ? bytes.ToArray().Reverse().ToArray()
            : bytes.ToArray());
    }

    public static byte[] GetBytes(ushort value)
    {
        var bytes = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }
        return bytes;
    }
}