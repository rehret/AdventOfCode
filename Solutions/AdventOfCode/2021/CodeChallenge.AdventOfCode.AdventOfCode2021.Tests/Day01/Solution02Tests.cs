namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Tests.Day01;

using CodeChallenge.AdventOfCode.AdventOfCode2021.Day01;
using CodeChallenge.Core.IO;

public class Solution02Tests
{
    private readonly Solution02 _solution;

    public Solution02Tests()
    {
        _solution = new Solution02(new Mock<IInputProvider<AdventOfCodeChallengeSelection, int>>().Object);
    }

    [Fact]
    public async Task ComputeSolutionAsync_GivenSampleInput_ProducesSampleOutput()
    {
        // Arrange
        var input = new[]
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

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(5, result);
    }
}