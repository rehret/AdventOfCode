namespace CodeChallenge.TomsDataOnion.Solutions.Layer4.Helpers;

public static class Checksum
{
    public static bool VerifyChecksum(ReadOnlySpan<byte> bytes)
    {
        uint sum = 0;
        for (var i = 0; i < bytes.Length - 1; i += 2)
        {
            sum += BigEndianBitConverter.ToUInt16(bytes[i..(i + 2)]);
        }

        if (bytes.Length % 2 == 1)
        {
            sum += bytes[^1];
        }

        while (sum >> 16 > 0)
        {
            sum = (sum & 0x0000ffff) + (sum >> 16);
        }

        // Cast sum to a ushort, take the one's complement, cast to ushort again (because NOT returns int)
        // and check if the result is zero. When the header (including received checksum) is valid, this
        // result will be zero.
        return (ushort)~(ushort)sum == 0;
    }
}