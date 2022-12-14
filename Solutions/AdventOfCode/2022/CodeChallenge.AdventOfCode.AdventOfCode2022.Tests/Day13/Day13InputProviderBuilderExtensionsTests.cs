namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day13;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day13;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day13.Models;
using CodeChallenge.Core.IO;

public class Day13InputProviderBuilderExtensionsTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly IInputProviderBuilder<AdventOfCodeChallengeSelection> _inputProviderBuilder;

    public Day13InputProviderBuilderExtensionsTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(_inputReaderMock.Object);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_ParsesPacketDataTuples()
    {
        // Arrange
        var input = new[]
        {
            "[1,1,3,1,1]",
            "[1,1,5,1,1]",
            "",
            "[[1],[2,3,4]]",
            "[[1],4]",
            "",
            "[9]",
            "[[8,7,6]]",
            "",
            "[[4,4],4,4]",
            "[[4,4],4,4,4]",
            "",
            "[7,7,7,7]",
            "[7,7,7]",
            "",
            "[]",
            "[3]",
            "",
            "[[[]]]",
            "[[]]",
            "",
            "[1,[2,[3,[4,[5,6,7]]]],8,9]",
            "[1,[2,[3,[4,[5,6,0]]]],8,9]"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        // Act
        var result = await _inputProviderBuilder.BuildDay13InputProvider()
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        // Assert
        var expected = Day13TestHelpers.GetSampleInput();
        Assert.Equal(expected, result);
    }
}