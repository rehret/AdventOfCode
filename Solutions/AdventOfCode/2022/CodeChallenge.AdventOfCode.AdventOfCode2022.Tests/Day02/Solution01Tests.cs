namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;
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
        var input = new List<StrategyGuideStep>
        {
            new(RockPaperScissorsMove.Paper, RockPaperScissorsMove.Rock),
            new(RockPaperScissorsMove.Rock, RockPaperScissorsMove.Paper),
            new(RockPaperScissorsMove.Scissors, RockPaperScissorsMove.Scissors)
        };

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(15, result);
    }
}