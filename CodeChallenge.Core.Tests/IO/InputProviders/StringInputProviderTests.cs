namespace CodeChallenge.Core.Tests.IO.InputProviders;

using CodeChallenge.Core;
using CodeChallenge.Core.IO;
using CodeChallenge.Core.IO.InputProviders;

public class StringInputProviderTests
{
    private readonly Mock<IInputReader<ChallengeSelection>> _inputReaderMock;
    private readonly StringInputProvider<ChallengeSelection> _inputProvider;

    public StringInputProviderTests()
    {
        _inputReaderMock = new Mock<IInputReader<ChallengeSelection>>();
        _inputProvider = new StringInputProvider<ChallengeSelection>(_inputReaderMock.Object);
    }

    [Theory]
    [InlineData("Test String", "Test String")]
    public async Task GetInputAsync_GivenValidInput_ReturnsInputString(string expected, string input)
    {
        // Arrange
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<ChallengeSelection>()))
            .ReturnsAsync(() => new[] { input });

        // Act
        var result = (await _inputProvider.GetInputAsync(new ChallengeSelection()).ConfigureAwait(false)).First();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task GetInputAsync_GivenEmptyInput_FiltersEmptyInputFromResult(string input)
    {
        // Arrange
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<ChallengeSelection>()))
            .ReturnsAsync(() => new[] { input });

        // Act
        var result = (await _inputProvider.GetInputAsync(new ChallengeSelection()).ConfigureAwait(false));

        // Assert
        Assert.Empty(result);
    }
}