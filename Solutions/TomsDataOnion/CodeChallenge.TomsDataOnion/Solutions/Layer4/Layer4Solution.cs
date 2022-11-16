namespace CodeChallenge.TomsDataOnion.Solutions.Layer4;

using CodeChallenge.TomsDataOnion.Solutions.Layer4.Helpers;
using CodeChallenge.TomsDataOnion.Solutions.Layer4.Models;

[TomsDataOnionSolution(4)]
internal class Layer4Solution : TomsDataOnionSolution
{
    private static readonly InternetProtocolV4Address AcceptedSourceIpAddress = new("10.1.1.10");
    private static readonly InternetProtocolV4Address AcceptedDestinationIpAddress = new("10.1.1.200");
    private static readonly ushort AcceptedDestinationPort = 42069;

    public Layer4Solution(IInputProvider<TomsDataOnionChallengeSelection, byte> inputProvider, ITomsDataOnionOutputWriter outputWriter)
        : base(inputProvider, outputWriter)
    { }

    protected override IEnumerable<byte> Decode(IEnumerable<byte> input)
    {
        return AcceptPackets(input.ToArray());
    }

    private static IEnumerable<byte> AcceptPackets(ReadOnlySpan<byte> bytes)
    {
        var acceptedBytes = Enumerable.Empty<byte>();

        while (bytes.Length != 0)
        {
            var ipV4PacketLength = IPv4PacketHelpers.GetPacketLength(bytes);

            if (ipV4PacketLength < 20)
            {
                // Invalid header length
                // Because of this, we can't trust anything else about the data, so we continually try
                // again at the next byte until we find a valid IPv4 header.
                bytes = bytes[1..];
                continue;
            }

            if (ipV4PacketLength > bytes.Length)
            {
                // Because we're occasionally only shifting by 1 byte (when we get an invalid IPv4 header)
                // we can end up with ipV4PacketLength being longer than the remaining bytes.
                // This is because we're actually in the middle the last packet and it did not contain
                // a valid header, so the computed header length is just some value in the middle of the packet.
                bytes = bytes[1..];
                continue;
            }

            var packet = bytes[..ipV4PacketLength];
            var remainingBytes = bytes[ipV4PacketLength..];

            if (!IPv4PacketHelpers.VerifyChecksum(packet))
            {
                // The header checksum is invalid, so we can't verify that the given packet length is correct.
                // As a result, we need to just continually try again at the next packet until we find another
                // valid packet.
                bytes = bytes[1..];
                continue;
            }

            var sourceIpAddress = IPv4PacketHelpers.GetSourceIpAddress(packet);
            if (sourceIpAddress != AcceptedSourceIpAddress)
            {
                // Source IP doesn't match our expected value, so discard the current packet and continue with
                // the remaining bytes.
                bytes = remainingBytes;
                continue;
            }

            var destinationIpAddress = IPv4PacketHelpers.GetDestinationIpAddress(packet);
            if (destinationIpAddress != AcceptedDestinationIpAddress)
            {
                // Destination IP doesn't match our expected value, so discard the current packet and continue
                // with the remaining bytes.
                bytes = remainingBytes;
                continue;
            }

            var udpPacket = IPv4PacketHelpers.GetBody(packet);

            if (!UdpPacketHelpers.VerifyChecksum(udpPacket, sourceIpAddress, destinationIpAddress))
            {
                // UDP header checksum is invalid, so discard the current packet and continue with the
                // remaining bytes.
                // We don't shift by only 1 byte here because we know the correct length of the entire
                // IPv4 packet.
                bytes = remainingBytes;
                continue;
            }

            var destinationUdpPort = UdpPacketHelpers.GetDestinationPort(udpPacket);
            if (destinationUdpPort != AcceptedDestinationPort)
            {
                // Destination port doesn't match our expected value, so discard the current packet and
                // continue with the remaining bytes.
                bytes = remainingBytes;
                continue;
            }

            // Packet accepted
            acceptedBytes = acceptedBytes.Concat(UdpPacketHelpers.GetPacketBody(udpPacket).ToArray());
            bytes = remainingBytes;
        }

        return acceptedBytes;
    }
}