namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day13;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day13.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 13, 2)]
internal class Solution02 : AdventOfCodeSolution<IEnumerable<(PacketData Left, PacketData Right)>, int>
{
    public Solution02(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay13InputProvider())
    { }

    protected override int ComputeSolution(IEnumerable<(PacketData Left, PacketData Right)> input)
    {
        var dividerPacket1 = new ListPacketData(new PacketData[] { new ListPacketData(new PacketData[] { new IntegerPacketData(2) }) });
        var dividerPacket2 = new ListPacketData(new PacketData[] { new ListPacketData(new PacketData[] { new IntegerPacketData(6) }) });

        var sortedPackets = input
            .SelectMany(x => new[] { x.Left, x.Right })
            .Append(dividerPacket1)
            .Append(dividerPacket2)
            .Order()
            .ToArray();

        var index1 = Array.IndexOf(sortedPackets, dividerPacket1);
        var index2 = Array.IndexOf(sortedPackets, dividerPacket2);

        // Indices in the problem start at 1
        return (index1 + 1) * (index2 + 1);
    }
}
