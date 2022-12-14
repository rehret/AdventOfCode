namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day12;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day12;
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
        var input = Day12TestHelpers.BuildSampleInput(Day12InputProviderBuilderExtensions.DijkstraType.StartIsSource);

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(31, result);
    }
}