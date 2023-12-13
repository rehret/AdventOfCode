namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Tests.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day02;
using CodeChallenge.AdventOfCode.AdventOfCode2023.Day02.Models;
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
        var input = new List<Game>
        {
            new(1, new List<DiceSet>
            {
                new(4, 0, 3),
                new(1, 2, 6),
                new(0, 2, 0)
            }),
            new(2, new List<DiceSet>
            {
                new(0, 2, 1),
                new(1, 3, 4),
                new(0, 1, 1)
            }),
            new(3, new List<DiceSet>
            {
                new(20, 8, 6),
                new(4, 13, 5),
                new(1, 5, 0)
            }),
            new(4, new List<DiceSet>
            {
                new(3, 1, 6),
                new(6, 3, 0),
                new(14, 3, 15)
            }),
            new(5, new List<DiceSet>
            {
                new(6, 3, 1),
                new(1, 2, 2)
            })
        };

        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        Assert.Equal(8, result);
    }
}