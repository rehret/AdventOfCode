namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day05.InputProviders;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day05.InputProviders;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day05.Models;
using CodeChallenge.Core.IO;

public class StacksAndInstructionsInputProviderTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly StacksAndInstructionsInputProvider _inputProvider;

    public StacksAndInstructionsInputProviderTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProvider = new StacksAndInstructionsInputProvider(_inputReaderMock.Object);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_ParsesIntoStacksAndInstructions()
    {
        // Arrange
        var input = new[]
        {
            "    [D]           ",
            "[N] [C]           ",
            "[Z] [M] [P]       ",
            " 1   2   3        ",
            "                  ",
            "move 1 from 2 to 1",
            "move 3 from 1 to 3",
            "move 2 from 2 to 1",
            "move 1 from 1 to 2"
        };

        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(() => input);

        // Act
        var result = await _inputProvider.GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0)).ConfigureAwait(false);

        // Assert
        Assert.Collection(
            result.Stacks,
            x =>
            {
                Assert.Equal('N', x.Pop());
                Assert.Equal('Z', x.Pop());
            },
            x =>
            {
                Assert.Equal('D', x.Pop());
                Assert.Equal('C', x.Pop());
                Assert.Equal('M', x.Pop());
            },
            x =>
            {
                Assert.Equal('P', x.Pop());
            }
        );

        Assert.Collection(
            result.Instructions,
            x => Assert.Equal(new MoveInstruction(1, 1, 0), x),
            x => Assert.Equal(new MoveInstruction(3, 0, 2), x),
            x => Assert.Equal(new MoveInstruction(2, 1, 0), x),
            x => Assert.Equal(new MoveInstruction(1, 0, 1), x)
        );
    }
}