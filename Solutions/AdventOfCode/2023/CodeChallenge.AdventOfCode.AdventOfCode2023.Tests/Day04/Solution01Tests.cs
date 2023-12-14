namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Tests.Day04;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day04;
using CodeChallenge.AdventOfCode.AdventOfCode2023.Day04.Models;
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
        var input = new List<ScratchCard>
        {
            new(1, new[] { 41, 48, 83, 86, 17 }, new[] { 83, 86, 6, 31, 17, 9, 48, 53 }),
            new(2, new[] { 13, 32, 20, 16, 61 }, new[] { 61, 30, 68, 82, 17, 32, 24, 19 }),
            new(3, new[] { 1, 21, 53, 59, 44 }, new[] { 69, 82, 63, 72, 16, 21, 14, 1 }),
            new(4, new[] { 41, 92, 73, 84, 69 }, new[] { 59, 84, 76, 51, 58, 5, 54, 83 }),
            new(5, new[] { 87, 83, 26, 28, 32 }, new[] { 88, 30, 70, 12, 93, 22, 82, 36 }),
            new(6, new[] { 31, 18, 13, 56, 72 }, new[] { 74, 77, 10, 23, 35, 67, 36, 11 })
        };

        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        Assert.Equal(13, result);
    }
}