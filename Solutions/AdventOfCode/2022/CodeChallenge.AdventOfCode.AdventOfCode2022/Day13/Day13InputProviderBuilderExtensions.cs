namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day13;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day13.Models;
using CodeChallenge.Core.IO;

internal static class Day13InputProviderBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<(PacketData Left, PacketData Right)>> BuildDay13InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadChunks(string.IsNullOrWhiteSpace)
            .ParseUsing(chunk =>
            {
                var items = chunk.ToArray();
                if (items.Length != 2)
                {
                    throw new FormatException($"Unexpected number of Packet Data items in pair: {items.Length}");
                }

                return (BuildPacketData(items[0]), BuildPacketData(items[1]));
            })
            .Build();
    }

    private static PacketData BuildPacketData(string packet)
    {
        var stack = new Stack<ListPacketData>();

        var index = 0;
        while (index < packet.Length)
        {
            switch (packet[index])
            {
                case ',':
                {
                    index++;
                    break;
                }
                case '[':
                {
                    stack.Push(new ListPacketData(Array.Empty<PacketData>()));
                    index++;
                    break;
                }
                case ']':
                {
                    var list = stack.Pop();
                    if (stack.Count == 0)
                    {
                        return list;
                    }
                    var head = new ListPacketData(stack.Pop().Data.Append(list).ToArray());
                    stack.Push(head);
                    index++;
                    break;
                }
                default:
                {
                    var nonNumberIndex = index + 1;
                    while (char.IsNumber(packet[nonNumberIndex]))
                    {
                        nonNumberIndex++;
                    }
                    var value = int.Parse(packet[index..nonNumberIndex]);
                    var head = new ListPacketData(stack.Pop().Data.Append(new IntegerPacketData(value)).ToArray());
                    stack.Push(head);
                    index = packet[nonNumberIndex] == ',' ? nonNumberIndex + 1 : nonNumberIndex;
                    break;
                }
            }
        }

        var result = stack.Pop();
        while (stack.Count != 0)
        {
            result = stack.Pop();
        }

        return result;
    }
}