namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day06;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day06;
using CodeChallenge.Core.IO;

public class Solution02Tests
{
    private readonly Solution02 _solution;

    public Solution02Tests()
    {
        var inputProviderMock = new Mock<IInputProvider<AdventOfCodeChallengeSelection, string>>();
        _solution = new Solution02(inputProviderMock.Object);
    }

    [Fact]
    public async Task ComputeSolutionAsync_WithSampleInput_ProducesSampleOutput()
    {
        // Arrange
        const string input = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(19, result);
    }

    [Theory]
    [InlineData(23, "bvwbjplbgvbhsrlpgdmjqwftvncz")]
    [InlineData(23, "nppdvjthqldpwncqszvftbrmjlhg")]
    [InlineData(29, "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg")]
    [InlineData(26, "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw")]
    public async Task ComputeSolutionAsync_WithExtraSampleInputs_ProducesSampleOutput(int expected, string input)
    {
        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(expected, result);
    }
}