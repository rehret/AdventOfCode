namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Tests.Day04;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day04;
using CodeChallenge.AdventOfCode.AdventOfCode2023.Day04.Models;
using CodeChallenge.Core.IO;

public class Day04InputProviderBuilderExtensionsTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly IInputProviderBuilder<AdventOfCodeChallengeSelection> _inputProviderBuilder;

    public Day04InputProviderBuilderExtensionsTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(_inputReaderMock.Object);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_ParsesScratchCardsFromLinesOfText()
    {
        var input = new[]
        {
            "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
            "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
            "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
            "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
            "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
            "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        var result = await _inputProviderBuilder.BuildDay04InputProvider()
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        var expected = new List<ScratchCard>
        {
            new(1, new[] { 41, 48, 83, 86, 17 }, new[] { 83, 86, 6, 31, 17, 9, 48, 53 }),
            new(2, new[] { 13, 32, 20, 16, 61 }, new[] { 61, 30, 68, 82, 17, 32, 24, 19 }),
            new(3, new[] { 1, 21, 53, 59, 44 }, new[] { 69, 82, 63, 72, 16, 21, 14, 1 }),
            new(4, new[] { 41, 92, 73, 84, 69 }, new[] { 59, 84, 76, 51, 58, 5, 54, 83 }),
            new(5, new[] { 87, 83, 26, 28, 32 }, new[] { 88, 30, 70, 12, 93, 22, 82, 36 }),
            new(6, new[] { 31, 18, 13, 56, 72 }, new[] { 74, 77, 10, 23, 35, 67, 36, 11 })
        };

        Assert.Collection(result,
            expected.Select<ScratchCard, Action<ScratchCard>>(expectedCard => card =>
            {
                Assert.Equal(expectedCard.CardNumber, card.CardNumber);
                Assert.Equal(expectedCard.WinningNumbers, card.WinningNumbers);
                Assert.Equal(expectedCard.ScratchedNumbers, card.ScratchedNumbers);
            }).ToArray()
        );
    }
}