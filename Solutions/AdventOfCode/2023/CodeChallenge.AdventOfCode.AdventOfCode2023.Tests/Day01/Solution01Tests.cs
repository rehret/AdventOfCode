namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Tests.Day01;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day01;
using CodeChallenge.Core.IO;

public class Solution01Tests
{
    private readonly Solution01 _solution;

    public Solution01Tests()
    {
        var inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        var inputBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(inputReaderMock.Object);
        _solution = new Solution01(inputBuilder);
    }

    [Fact]
    public async Task ComputeSolutionAsync_WithSampleInput_ProducesSampleOutput()
    {
        var input = new List<IEnumerable<int>>
        {
            new List<int> { 1, 2 },
            new List<int> { 3, 8 },
            new List<int> { 1, 2, 3, 4, 5 },
            new List<int> { 7 }
        };

        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        Assert.Equal(142, result);
    }
}