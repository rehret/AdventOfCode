namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Tests.Day03;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day03;
using CodeChallenge.AdventOfCode.AdventOfCode2023.Day03.Models;
using CodeChallenge.Core.IO;

public class Day03InputProviderBuilderExtensionsTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly IInputProviderBuilder<AdventOfCodeChallengeSelection> _inputProviderBuilder;

    public Day03InputProviderBuilderExtensionsTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(_inputReaderMock.Object);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_ParsesSchematicFromText()
    {
        var input = new[]
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        var result = await _inputProviderBuilder.BuildDay03InputProvider()
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        var expected = new Schematic(
            new List<Part>
            {
                new(467, '*', (X: 3, Y: 1)),
                new(35, '*', (X: 3, Y: 1)),
                new(633, '#', (X: 6, Y: 3)),
                new(617, '*', (X: 3, Y: 4)),
                new(592, '+', (X: 5, Y: 5)),
                new(755, '*', (X: 5, Y: 8)),
                new(664, '$', (X: 3, Y: 8)),
                new(598, '*', (X: 5, Y: 8))
            },
            new List<int>
            {
                114,
                58
            }
        );

        Assert.Collection(result.Parts,
            expected.Parts.Select<Part, Action<Part>>(expectedPart => part =>
            {
                Assert.Equal(expectedPart.PartNumber, part.PartNumber);
                Assert.Equal(expectedPart.Symbol, part.Symbol);
                Assert.Equal(expectedPart.SymbolCoordinate, part.SymbolCoordinate);
            }).ToArray()
        );

        Assert.Collection(result.DanglingLabels,
            expected.DanglingLabels.Select<int, Action<int>>(expectedLabel => label => Assert.Equal(expectedLabel, label)).ToArray()
        );
    }
}