namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day15;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day15;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day15.Models;
using CodeChallenge.Core.IO;

public class Solution02Tests
{
    private readonly Solution02 _solution;

    public Solution02Tests()
    {
        var inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        var inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(inputReaderMock.Object);
        _solution = new Solution02(inputProviderBuilder, new Coordinate(0, 0), new Coordinate(20, 20));
    }

    [Fact]
    public async Task ComputeSolutionAsync_WithSampleInput_ProducesSampleOutput()
    {
        // Arrange
        var input = Day15TestHelpers.BuildSampleInput();

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(56000011, result);
    }
}