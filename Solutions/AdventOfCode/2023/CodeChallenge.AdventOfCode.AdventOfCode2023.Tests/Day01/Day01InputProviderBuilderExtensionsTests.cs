namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Tests.Day01;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day01;
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
    public async Task Puzzle01_GetInputAsync_GivenSampleInput_ParsesIntegersFromLinesOfText()
    {
        var input = new[]
        {
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        var result = await _inputProviderBuilder.BuildDay01Puzzle01InputProvider()
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        var expected = new List<IEnumerable<int>>
        {
            new List<int> { 1, 2 },
            new List<int> { 3, 8 },
            new List<int> { 1, 2, 3, 4, 5 },
            new List<int> { 7 }
        };
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task Puzzle02_GetInputAsync_GivenSampleInput_ParsesIntegersAndSpelledOutIntegersFromLinesOfText()
    {
        var input = new[]
        {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        var result = await _inputProviderBuilder.BuildDay01Puzzle02InputProvider()
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        var expected = new List<IEnumerable<int>>
        {
            new List<int> { 2, 1, 9 },
            new List<int> { 8, 2, 3 },
            new List<int> { 1, 2, 3 },
            new List<int> { 2, 1, 3, 4 },
            new List<int> { 4, 9, 8, 7, 2 },
            new List<int> { 1, 8, 2, 3, 4 },
            new List<int> { 7, 6 }
        };
        Assert.Equal(expected, result);
    }
}