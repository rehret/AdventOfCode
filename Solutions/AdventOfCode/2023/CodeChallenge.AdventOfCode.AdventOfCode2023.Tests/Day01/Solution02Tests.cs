namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Tests.Day01;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day01;
using CodeChallenge.Core.IO;

public class Solution02Tests
{
    private readonly Solution02 _solution;

    public Solution02Tests()
    {
        var inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        var inputBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(inputReaderMock.Object);
        _solution = new Solution02(inputBuilder);
    }

    [Fact]
    public async Task ComputeSolutionAsync_WithSampleInput_ProducesSampleOutput()
    {
        var input = new List<IEnumerable<int>>
        {
            new List<int> { 2, 1, 9 },
            new List<int> { 8, 2, 3 },
            new List<int> { 1, 2, 3 },
            new List<int> { 2, 1, 3, 4 },
            new List<int> { 4, 9, 8, 7, 2 },
            new List<int> { 1, 8, 2, 3, 4 },
            new List<int> { 7, 6 }
        };

        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        Assert.Equal(281, result);
    }
}