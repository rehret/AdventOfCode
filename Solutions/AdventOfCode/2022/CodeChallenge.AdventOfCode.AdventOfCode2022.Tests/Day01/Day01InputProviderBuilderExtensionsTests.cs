namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day01;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day01;
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
    public async Task GetInputAsync_GivenSampleInput_ParsesIntegerChunks()
    {
        // Arrange
        var input = new[]
        {
            "1000",
            "2000",
            "3000",
            "",
            "4000",
            "",
            "5000",
            "6000",
            "",
            "7000",
            "8000",
            "9000",
            "",
            "10000"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        // Act
        var result = await _inputProviderBuilder.BuildDay01InputProvider()
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        // Assert
        var expected = new List<IEnumerable<int>>
        {
            new List<int> { 1000, 2000, 3000 },
            new List<int> { 4000 },
            new List<int> { 5000, 6000 },
            new List<int> { 7000, 8000, 9000 },
            new List<int> { 10000 }
        };
        Assert.Equal(expected, result);
    }
}