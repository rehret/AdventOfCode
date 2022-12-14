namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day14;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day14;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day14.Models;
using CodeChallenge.Core.IO;

public class Day14InputProviderBuilderExtensionsTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly IInputProviderBuilder<AdventOfCodeChallengeSelection> _inputProviderBuilder;

    public Day14InputProviderBuilderExtensionsTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(_inputReaderMock.Object);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_ParsesIntoGrid()
    {
        // Arrange
        var input = new[]
        {
            "498,4 -> 498,6 -> 496,6",
            "503,4 -> 502,4 -> 502,9 -> 494,9"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        // Act
        var result = await _inputProviderBuilder.BuildDay14InputProvider()
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        // Assert
        var expected = new Material[10][];
        for (var i = 0; i < expected.Length; i++)
        {
            expected[i] = new Material[503 * 2];
            Array.Fill(expected[i], Material.Air);
        }

        expected[4][498] = Material.Rock;
        expected[5][498] = Material.Rock;
        expected[6][498] = Material.Rock;
        expected[6][497] = Material.Rock;
        expected[6][496] = Material.Rock;

        expected[4][503] = Material.Rock;
        expected[4][502] = Material.Rock;
        expected[5][502] = Material.Rock;
        expected[6][502] = Material.Rock;
        expected[7][502] = Material.Rock;
        expected[8][502] = Material.Rock;
        expected[9][502] = Material.Rock;
        expected[9][501] = Material.Rock;
        expected[9][500] = Material.Rock;
        expected[9][499] = Material.Rock;
        expected[9][498] = Material.Rock;
        expected[9][497] = Material.Rock;
        expected[9][496] = Material.Rock;
        expected[9][495] = Material.Rock;
        expected[9][494] = Material.Rock;

        Assert.Equal(expected, result);
    }
}