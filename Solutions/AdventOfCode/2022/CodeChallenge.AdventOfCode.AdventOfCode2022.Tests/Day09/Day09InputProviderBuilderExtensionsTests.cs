namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day09;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day09;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day09.Models;
using CodeChallenge.Core.IO;

public class Day09InputProviderBuilderExtensionsTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly IInputProviderBuilder<AdventOfCodeChallengeSelection> _inputProviderBuilder;

    public Day09InputProviderBuilderExtensionsTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(_inputReaderMock.Object);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_ParsesMoveInstructions()
    {
        // Arrange
        var input = new[]
        {
            "R 4",
            "U 4",
            "L 3",
            "D 1",
            "R 4",
            "D 1",
            "L 5",
            "R 2"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        // Act
        var result = await _inputProviderBuilder.BuildDay09InputProvider()
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        // Assert
        var expected = new MoveInstruction[]
        {
            new(MoveDirection.Right, 4),
            new(MoveDirection.Up, 4),
            new(MoveDirection.Left, 3),
            new(MoveDirection.Down, 1),
            new(MoveDirection.Right, 4),
            new(MoveDirection.Down, 1),
            new(MoveDirection.Left, 5),
            new(MoveDirection.Right, 2)
        };
        Assert.Equal(expected, result);
    }
}