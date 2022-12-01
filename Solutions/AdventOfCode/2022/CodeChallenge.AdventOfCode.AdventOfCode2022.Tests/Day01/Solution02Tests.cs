namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day01;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day01;
using CodeChallenge.Core.IO;

public class Solution02Tests
{
    private readonly Solution02 _solution;

    public Solution02Tests()
    {
        var inputProviderMock = new Mock<IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<IEnumerable<int>>>>();
        _solution = new Solution02(inputProviderMock.Object);
    }

    [Fact]
    public async Task ComputeSolutionAsync_WithSampleInput_ProducesSampleOutput()
    {
        // Arrange
        var input = new List<IEnumerable<int>>
        {
            new List<int> { 1000, 2000, 3000 },
            new List<int> { 4000 },
            new List<int> { 5000, 6000 },
            new List<int> { 7000, 8000, 9000 },
            new List<int> { 10000 }
        };

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(45000, result);
    }
}