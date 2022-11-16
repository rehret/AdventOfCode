namespace AdventOfCode.Tests.InputProviders;

using AdventOfCode.InputProviders;

public class IntInputProcessorTests
{
    private readonly Mock<IInputReader> _inputReaderMock;
    private readonly IntInputProvider _inputProvider;

    public IntInputProcessorTests()
    {
        _inputReaderMock = new Mock<IInputReader>();
        _inputProvider = new IntInputProvider(_inputReaderMock.Object);
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(100, "100")]
    [InlineData(0, "0")]
    [InlineData(-1, "-1")]
    [InlineData(-100, "-100")]
    public async Task GetInputAsync_GivenValidInput_CorrectlyParsesInt(int expected, string input)
    {
        // Arrange
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<PuzzleSelection>()))
            .ReturnsAsync(() => new[] { input });

        // Act
        var result = (await _inputProvider.GetInputAsync(new PuzzleSelection(0 , 0, 0)).ConfigureAwait(false)).First();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetInputAsync_GivenInvalidInput_ThrowsIntInputProcessorParsingException()
    {
        // Arrange
        const string input = "A";
        // Arrange
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<PuzzleSelection>()))
            .ReturnsAsync(() => new[] { input });

        // Act & Assert
        Assert.ThrowsAsync<IntInputProcessorParsingException>(async () => (await _inputProvider.GetInputAsync(new PuzzleSelection(0, 0, 0)).ConfigureAwait(false)).ToArray());
    }
}