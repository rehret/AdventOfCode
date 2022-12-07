namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day06;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day06;
using CodeChallenge.Core.IO;

public class Solution01Tests
{
    private readonly Solution01 _solution;

    public Solution01Tests()
    {
        var inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        var inputBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(inputReaderMock.Object);
        _solution = new Solution01(inputBuilder);
    }

    [Fact]
    public async Task ComputeSolutionAsync_WithSampleInput_ProducesSampleOutput()
    {
        // Arrange
        const string input = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(7, result);
    }

    [Theory]
    [InlineData(5, "bvwbjplbgvbhsrlpgdmjqwftvncz")]
    [InlineData(6, "nppdvjthqldpwncqszvftbrmjlhg")]
    [InlineData(10, "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg")]
    [InlineData(11, "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw")]
    public async Task ComputeSolutionAsync_WithExtraSampleInputs_ProducesSampleOutput(int expected, string input)
    {
        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(expected, result);
    }
}