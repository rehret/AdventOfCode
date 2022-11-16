namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.Helpers;

public class LittleEndianBitConverter
{
    public static uint ToUInt32(ReadOnlySpan<byte> bytes)
    {
        return BitConverter.ToUInt32(BitConverter.IsLittleEndian
            ? bytes.ToArray()
            : bytes.ToArray().Reverse().ToArray());
    }
}