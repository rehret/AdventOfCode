namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Tests.Day01;

using CodeChallenge.AdventOfCode.AdventOfCode2021.Day01;
using CodeChallenge.Core.IO;

public class Day01InputProviderBuilderExtensionsTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly IInputProviderBuilder<AdventOfCodeChallengeSelection> _inputProviderBuilder;

    public Day01InputProviderBuilderExtensionsTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(_inputReaderMock.Object);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_ParsesIntegers()
    {
        // Arrange
        var input = new[]
        {
            "199",
            "200",
            "208",
            "210",
            "200",
            "207",
            "240",
            "269",
            "260",
            "263"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        // Act
        var result = await _inputProviderBuilder.BuildDay01InputProvider()
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        // Assert
        var expected = new[]
        {
            199,
            200,
            208,
            210,
            200,
            207,
            240,
            269,
            260,
            263
        };
        Assert.Equal(expected, result);
    }
}