namespace AdventOfCode.Tests.InputProviders;

using AdventOfCode.InputProviders;

public class StringInputProcessorTests
{
    private readonly Mock<IInputReader> _inputReaderMock;
    private readonly StringInputProvider _inputProvider;

    public StringInputProcessorTests()
    {
        _inputReaderMock = new Mock<IInputReader>();
        _inputProvider = new StringInputProvider(_inputReaderMock.Object);
    }

    [Theory]
    [InlineData("Test String", "Test String")]
    [InlineData("", "")]
    public async Task GetInputAsync_GivenValidInput_ReturnsInputString(string expected, string input)
    {
        // Arrange
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<PuzzleSelection>()))
            .ReturnsAsync(() => new[] { input });

        // Act
        var result = (await _inputProvider.GetInputAsync(new PuzzleSelection(0, 0, 0)).ConfigureAwait(false)).First();

        // Assert
        Assert.Equal(expected, result);
    }
}