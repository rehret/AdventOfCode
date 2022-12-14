namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day02.Models;
using CodeChallenge.Core.IO;

public class Day02InputProviderBuilderExtensionsTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly IInputProviderBuilder<AdventOfCodeChallengeSelection> _inputProviderBuilder;

    public Day02InputProviderBuilderExtensionsTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(_inputReaderMock.Object);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_WhenTypeIsSuggestedMove_ParsesStrategyGuideSteps()
    {
        // Arrange
        var input = new[]
        {
            "A Y",
            "B X",
            "C Z"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        // Act
        var result = await _inputProviderBuilder
            .BuildDay02InputProvider(StrategyGuideStepInputProviderType.SuggestedMove)
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        // Assert
        var expected = new List<StrategyGuideStep>
        {
            new(RockPaperScissorsMove.Paper, RockPaperScissorsMove.Rock),
            new(RockPaperScissorsMove.Rock, RockPaperScissorsMove.Paper),
            new(RockPaperScissorsMove.Scissors, RockPaperScissorsMove.Scissors)
        };
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_WhenTypeIsTargetResult_ParsesStrategyGuideSteps()
    {
        // Arrange
        var input = new[]
        {
            "A Y",
            "B X",
            "C Z"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        // Act
        var result = await _inputProviderBuilder
            .BuildDay02InputProvider(StrategyGuideStepInputProviderType.TargetResult)
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        // Assert
        var expected = new List<StrategyGuideStep>
        {
            new(RockPaperScissorsResult.Draw, RockPaperScissorsMove.Rock),
            new(RockPaperScissorsResult.Lose, RockPaperScissorsMove.Paper),
            new(RockPaperScissorsResult.Win, RockPaperScissorsMove.Scissors)
        };
        Assert.Equal(expected, result);
    }
}