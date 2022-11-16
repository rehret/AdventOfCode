namespace CodeChallenge.TomsDataOnion.Solutions.Layer4.Helpers;

using CodeChallenge.TomsDataOnion.Solutions.Layer4.Models;

public static class IPv4PacketHelpers
{
    public static ushort GetPacketLength(ReadOnlySpan<byte> bytes)
    {
        return BigEndianBitConverter.ToUInt16(bytes[2..4]);
    }

    public static ushort GetHeaderLength(ReadOnlySpan<byte> packet)
    {
        return (byte)((packet[0] & 0x0F) * 4); // IHL is the number of 4-Byte words, so multiply its value by 4 for the number of bytes
    }

    public static ReadOnlySpan<byte> GetHeader(ReadOnlySpan<byte> packet)
    {
        return packet[..GetHeaderLength(packet)];
    }

    public static ReadOnlySpan<byte> GetBody(ReadOnlySpan<byte> packet)
    {
        return packet[GetHeaderLength(packet)..];
    }

    public static bool VerifyChecksum(ReadOnlySpan<byte> packet)
    {
        return Checksum.VerifyChecksum(GetHeader(packet));
    }

    public static InternetProtocolV4Address GetSourceIpAddress(ReadOnlySpan<byte> packet)
    {
        return new InternetProtocolV4Address(packet[12..16].ToArray());
    }

    public static InternetProtocolV4Address GetDestinationIpAddress(ReadOnlySpan<byte> packet)
    {
        return new InternetProtocolV4Address(packet[16..20].ToArray());
    }
}