namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day13;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day13;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day13.Models;
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
        var input = Day13TestHelpers.GetSampleInput();

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(140, result);
    }
}