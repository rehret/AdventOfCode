namespace CodeChallenge.Core.Tests.InputProviders;

using CodeChallenge.Core;
using CodeChallenge.Core.IO;
using CodeChallenge.Core.IO.InputProviders;

public class IntInputProviderTests
{
    private readonly Mock<IInputReader<ChallengeSelection>> _inputReaderMock;
    private readonly IntInputProvider<ChallengeSelection> _inputProvider;

    public IntInputProviderTests()
    {
        _inputReaderMock = new Mock<IInputReader<ChallengeSelection>>();
        _inputProvider = new IntInputProvider<ChallengeSelection>(_inputReaderMock.Object);
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
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<ChallengeSelection>()))
            .ReturnsAsync(() => new[] { input });

        // Act
        var result = (await _inputProvider.GetInputAsync(new ChallengeSelection()).ConfigureAwait(false)).First();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetInputAsync_GivenInvalidInput_ThrowsIntInputProcessorParsingException()
    {
        // Arrange
        const string input = "A";
        // Arrange
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<ChallengeSelection>()))
            .ReturnsAsync(() => new[] { input });

        // Act & Assert
        Assert.ThrowsAsync<IntInputProcessorParsingException>(async () => (await _inputProvider.GetInputAsync(new ChallengeSelection()).ConfigureAwait(false)).ToArray());
    }
}