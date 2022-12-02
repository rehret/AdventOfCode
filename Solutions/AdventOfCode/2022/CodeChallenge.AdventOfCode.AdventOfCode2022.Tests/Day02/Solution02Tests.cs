namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.InputProviders;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;

public class Solution02Tests
{
    private readonly Solution02 _solution;
    public Solution02Tests()
    {
        var inputProviderMock = new Mock<IStrategyGuideStepInputProvider>();
        _solution = new Solution02(inputProviderMock.Object);
    }

    [Fact]
    public async Task ComputeSolutionAsync_WithSampleInput_ProducesSampleOutput()
    {
        // Arrange
        var input = new List<StrategyGuideStep>
        {
            new(RockPaperScissorsMove.Rock, TargetResult.Draw),
            new(RockPaperScissorsMove.Paper, TargetResult.Lose),
            new(RockPaperScissorsMove.Scissors, TargetResult.Win)
        };

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(12, result);
    }
}