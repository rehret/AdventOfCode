namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day13;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day13.Models;

internal static class Day13TestHelpers
{
    public static IEnumerable<(PacketData Left, PacketData Right)> GetSampleInput()
    {
        return new List<(PacketData Left, PacketData Right)>
        {
            (
                new ListPacketData(new IntegerPacketData(1),
                    new IntegerPacketData(1),
                    new IntegerPacketData(3),
                    new IntegerPacketData(1),
                    new IntegerPacketData(1)),
                new ListPacketData(new IntegerPacketData(1),
                    new IntegerPacketData(1),
                    new IntegerPacketData(5),
                    new IntegerPacketData(1),
                    new IntegerPacketData(1))
            ),
            (
                new ListPacketData(new ListPacketData(new IntegerPacketData(1)),
                    new ListPacketData(new IntegerPacketData(2), new IntegerPacketData(3), new IntegerPacketData(4))),
                new ListPacketData(new ListPacketData(new IntegerPacketData(1)), new IntegerPacketData(4))
            ),
            (
                new ListPacketData(new IntegerPacketData(9)),
                new ListPacketData(new ListPacketData(new IntegerPacketData(8),
                    new IntegerPacketData(7),
                    new IntegerPacketData(6)))
            ),
            (
                new ListPacketData(new ListPacketData(new IntegerPacketData(4), new IntegerPacketData(4)),
                    new IntegerPacketData(4),
                    new IntegerPacketData(4)),
                new ListPacketData(new ListPacketData(new IntegerPacketData(4), new IntegerPacketData(4)),
                    new IntegerPacketData(4),
                    new IntegerPacketData(4),
                    new IntegerPacketData(4))
            ),
            (
                new ListPacketData(new IntegerPacketData(7),
                    new IntegerPacketData(7),
                    new IntegerPacketData(7),
                    new IntegerPacketData(7)),
                new ListPacketData(new IntegerPacketData(7), new IntegerPacketData(7), new IntegerPacketData(7))
            ),
            (
                new ListPacketData(),
                new ListPacketData(new IntegerPacketData(3))
            ),
            (
                new ListPacketData(new ListPacketData(new ListPacketData())),
                new ListPacketData(new ListPacketData())
            ),
            (
                new ListPacketData(new IntegerPacketData(1),
                    new ListPacketData(new IntegerPacketData(2),
                        new ListPacketData(new IntegerPacketData(3),
                            new ListPacketData(new IntegerPacketData(4),
                                new ListPacketData(new IntegerPacketData(5),
                                    new IntegerPacketData(6),
                                    new IntegerPacketData(7))))),
                    new IntegerPacketData(8),
                    new IntegerPacketData(9)),
                new ListPacketData(new IntegerPacketData(1),
                    new ListPacketData(new IntegerPacketData(2),
                        new ListPacketData(new IntegerPacketData(3),
                            new ListPacketData(new IntegerPacketData(4),
                                new ListPacketData(new IntegerPacketData(5),
                                    new IntegerPacketData(6),
                                    new IntegerPacketData(0))))),
                    new IntegerPacketData(8),
                    new IntegerPacketData(9))
            )
        };
    }
}