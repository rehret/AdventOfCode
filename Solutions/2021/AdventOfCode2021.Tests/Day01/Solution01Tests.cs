namespace AdventOfCode2021.Tests.Day01;

using AdventOfCode2021.Day01;

using Microsoft.Extensions.Logging.Abstractions;

public class Solution01Tests
{
    private readonly Solution01 _solution;

    public Solution01Tests()
    {
        _solution = new Solution01(new Mock<IInputReader>().Object, new Mock<IInputProcessor<int>>().Object, new NullLoggerFactory());
    }

    [Fact]
    public async Task ComputeSolutionAsync_GivenSampleInput_ProducesSampleOutput()
    {
        // Arrange
        var input = new[]
        {
            199,
            200,
            208,
            210,
            200,
            207,
            240,
            269,
            260,
            263
        };

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(7, result);
    }
}