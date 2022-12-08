namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day08;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day08;
using CodeChallenge.Core.IO;

public class Solution01Tests
{
    private readonly Solution01 _solution;

    public Solution01Tests()
    {
        var inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        var inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(inputReaderMock.Object);
        _solution = new Solution01(inputProviderBuilder);
    }

    [Fact]
    public async Task ComputeSolutionAsync_WithSampleInput_ProducesSampleOutput()
    {
        // Arrange
        var input = new[]
        {
            new[] { 3, 0, 3, 7, 3 },
            new[] { 2, 5, 5, 1, 2 },
            new[] { 6, 5, 3, 3, 2 },
            new[] { 3, 3, 5, 4, 9 },
            new[] { 3, 5, 3, 9, 0 }
        };

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(21, result);
    }
}