namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Tests.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2021.Day02;
using CodeChallenge.AdventOfCode.AdventOfCode2021.Day02.Models;
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
    public async Task GetInputAsync_GivenSampleInput_ParsesSubmarineInstructions()
    {
        // Arrange
        var input = new[]
        {
            "forward 5",
            "down 5",
            "forward 8",
            "up 3",
            "down 8",
            "forward 2"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        // Act
        var result = await _inputProviderBuilder.BuildDay02InputProvider()
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        // Assert
        var expected = new SubmarineInstruction[]
        {
            new(SubmarineMovement.Forward, 5),
            new(SubmarineMovement.Down, 5),
            new(SubmarineMovement.Forward, 8),
            new(SubmarineMovement.Up, 3),
            new(SubmarineMovement.Down, 8),
            new(SubmarineMovement.Forward, 2)
        };
        Assert.Equal(expected, result);
    }
}