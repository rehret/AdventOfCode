namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day08;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day08;
using CodeChallenge.Core.IO;

public class Day08InputProviderBuilderExtensionsTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly IInputProviderBuilder<AdventOfCodeChallengeSelection> _inputProviderBuilder;

    public Day08InputProviderBuilderExtensionsTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(_inputReaderMock.Object);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_ParsesTreeHeights()
    {
        // Arrange
        var input = new[]
        {
            "30373",
            "25512",
            "65332",
            "33549",
            "35390"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        // Act
        var result = await _inputProviderBuilder.BuildDay08InputProvider()
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        // Assert
        var expected = new[]
        {
            new[] { 3, 0, 3, 7, 3 },
            new[] { 2, 5, 5, 1, 2 },
            new[] { 6, 5, 3, 3, 2 },
            new[] { 3, 3, 5, 4, 9 },
            new[] { 3, 5, 3, 9, 0 }
        };
        Assert.Equal(expected, result);
    }
}