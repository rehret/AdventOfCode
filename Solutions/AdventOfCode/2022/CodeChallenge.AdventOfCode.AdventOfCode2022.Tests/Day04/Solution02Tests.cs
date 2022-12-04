namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day04;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day04;
using CodeChallenge.Core.IO;

using Range = System.Range;

public class Solution02Tests
{
    private readonly Solution02 _solution;
    public Solution02Tests()
    {
        var inputProviderMock = new Mock<IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<(Range, Range)>>>();
        _solution = new Solution02(inputProviderMock.Object);
    }

    [Fact]
    public async Task ComputeSolutionAsync_WithSampleInput_ProducesSampleOutput()
    {
        // Arrange
        var input = new List<(Range, Range)>
        {
            (new Range(2, 5), new Range(6, 9)),
            (new Range(2, 4), new Range(4, 6)),
            (new Range(5, 8), new Range(7, 10)),
            (new Range(2, 9), new Range(3, 8)),
            (new Range(6, 7), new Range(4, 7)),
            (new Range(2, 7), new Range(4, 8))
        };

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(4, result);
    }
}