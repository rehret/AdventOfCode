namespace CodeChallenge.TomsDataOnion.Solutions.Layer4.Helpers;

using CodeChallenge.TomsDataOnion.Solutions.Layer4.Models;

public static class UdpPacketHelpers
{
    private const ushort UdpHeaderLength = 8;
    private const byte UdpProtocol = 17; // 0x11

    public static bool VerifyChecksum(ReadOnlySpan<byte> udpPacket, InternetProtocolV4Address sourceIpAddress, InternetProtocolV4Address destinationIpAddress)
    {
        var checksumPayload = Enumerable.Empty<byte>()
            .Concat(sourceIpAddress.Octets)
            .Concat(destinationIpAddress.Octets)
            .Append<byte>(0x00)
            .Append(UdpProtocol)
            .Concat(BigEndianBitConverter.GetBytes(GetUdpLength(udpPacket)))
            .Concat(udpPacket.ToArray());

        if (checksumPayload.Count() % 2 != 0)
        {
            checksumPayload = checksumPayload.Append<byte>(0x00);
        }

        return Checksum.VerifyChecksum(checksumPayload.ToArray().AsSpan());
    }

    public static ReadOnlySpan<byte> GetUdpHeader(ReadOnlySpan<byte> udpPacket)
    {
        return udpPacket[..UdpHeaderLength];
    }

    public static ReadOnlySpan<byte> GetPacketBody(ReadOnlySpan<byte> udpPacket)
    {
        return udpPacket[UdpHeaderLength..];
    }

    public static ushort GetUdpLength(ReadOnlySpan<byte> udpPacket)
    {
        return BigEndianBitConverter.ToUInt16(udpPacket[4..6]);
    }

    public static ushort GetDestinationPort(ReadOnlySpan<byte> udpPacket)
    {
        return BigEndianBitConverter.ToUInt16(udpPacket[2..4]);
    }
}