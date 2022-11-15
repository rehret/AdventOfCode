namespace AdventOfCode.Tests.InputProcessors;

using AdventOfCode.InputProcessors;

public class StringInputProcessorTests
{
    private readonly StringInputProcessor _inputProcessor;

    public StringInputProcessorTests()
    {
        _inputProcessor = new StringInputProcessor();
    }

    [Theory]
    [InlineData("Test String", "Test String")]
    [InlineData("", "")]
    public void Process_GivenValidInput_ReturnsInputString(string expected, string input)
    {
        // Act
        var result = _inputProcessor.Process(new[] { input }).First();

        // Assert
        Assert.Equal(expected, result);
    }
}