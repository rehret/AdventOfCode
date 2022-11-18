namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Tests.Day01;

using CodeChallenge.AdventOfCode.AdventOfCode2021.Day01;
using CodeChallenge.Core;

public class Solution01Tests
{
    private readonly Solution01 _solution;

    public Solution01Tests()
    {
        _solution = new Solution01(new Mock<IInputProvider<AdventOfCodeChallengeSelection, int>>().Object);
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
        Assert.Equal(7, result);
    }
}