namespace AdventOfCode.Tests.InputProcessors;

using AdventOfCode.InputProcessors;

public class IntInputProcessorTests
{
    private readonly IntInputProcessor _inputProcessor;

    public IntInputProcessorTests()
    {
        _inputProcessor = new IntInputProcessor();
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(100, "100")]
    [InlineData(0, "0")]
    [InlineData(-1, "-1")]
    [InlineData(-100, "-100")]
    public void Process_GivenValidInput_CorrectlyParsesInt(int expected, string input)
    {
        // Act
        var result = _inputProcessor.Process(new[] { input }).First();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Process_GivenInvalidInput_ThrowsIntInputProcessorParsingException()
    {
        // Arrange
        const string input = "A";

        // Act & Assert
        Assert.Throws<IntInputProcessorParsingException>(() => _inputProcessor.Process(new[] { input }).ToArray());
    }
}