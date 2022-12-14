namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day12;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day12;
using CodeChallenge.Core.IO;

public class Solution02Tests
{
    private readonly Solution02 _solution;

    public Solution02Tests()
    {
        var inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        var inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(inputReaderMock.Object);
        _solution = new Solution02(inputProviderBuilder);
    }

    [Fact]
    public async Task ComputeSolutionAsync_WithSampleInput_ProducesSampleOutput()
    {
        // Arrange
        var input = Day12TestHelpers.BuildSampleInput(Day12InputProviderBuilderExtensions.DijkstraType.EndIsSource);

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(29, result);
    }
}