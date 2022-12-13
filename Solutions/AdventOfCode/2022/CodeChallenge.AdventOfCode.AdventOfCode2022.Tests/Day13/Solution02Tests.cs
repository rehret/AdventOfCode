namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day13;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day13;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day13.Models;
using CodeChallenge.Core.IO;

public class Solution02Tests
{
    private readonly Solution02 _solution;

    public Solution02Tests()
    {
        var inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        var inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(inputReaderMock.Object);
        _solution = new Solution02(inputProviderBuilder);
    }

    [Fact]
    public async Task ComputeSolutionAsync_WithSampleInput_ProducesSampleOutput()
    {
        // Arrange
        var input = new List<(PacketData Left, PacketData Right)>
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

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(140, result);
    }
}