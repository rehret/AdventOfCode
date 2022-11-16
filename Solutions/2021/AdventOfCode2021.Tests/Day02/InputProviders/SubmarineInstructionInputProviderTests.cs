namespace AdventOfCode2021.Tests.Day02.InputProviders;

using AdventOfCode2021.Day02.InputProviders;
using AdventOfCode2021.Day02.Models;

public class SubmarineInstructionInputProviderTests
{
    private readonly Mock<IInputReader> _inputReaderMock;
    private readonly SubmarineInstructionInputProvider _inputProvider;

    public SubmarineInstructionInputProviderTests()
    {
        _inputReaderMock = new Mock<IInputReader>();
        _inputProvider = new SubmarineInstructionInputProvider(_inputReaderMock.Object);
    }

    [Theory]
    [MemberData(nameof(GetInputData))]
    internal async Task GetInputAsync_GivenValidInput_CorrectlyParsesSubmarineInstruction(SubmarineInstruction expected, string input)
    {
        // Arrange
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<PuzzleSelection>()))
            .ReturnsAsync(() => new[] { input });

        // Act
        var result = (await _inputProvider.GetInputAsync(new PuzzleSelection(0, 0, 0)).ConfigureAwait(false)).First();

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> GetInputData()
    {
        yield return new object[] { new SubmarineInstruction(SubmarineMovement.Forward, 1), "forward 1" };
        yield return new object[] { new SubmarineInstruction(SubmarineMovement.Up, 0), "up 0" };
        yield return new object[] { new SubmarineInstruction(SubmarineMovement.Down, 10), "down 10" };
    }
}