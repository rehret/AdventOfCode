namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Tests.Day03;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day03;
using CodeChallenge.AdventOfCode.AdventOfCode2023.Day03.Models;
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
        var input = new Schematic(
            new List<Part>
            {
                new(467, '*', (X: 3, Y: 1)),
                new(35, '*', (X: 3, Y: 1)),
                new(633, '#', (X: 6, Y: 3)),
                new(617, '*', (X: 3, Y: 4)),
                new(592, '+', (X: 5, Y: 5)),
                new(755, '*', (X: 5, Y: 8)),
                new(664, '$', (X: 3, Y: 8)),
                new(598, '*', (X: 5, Y: 8))
            },
            new List<int>
            {
                114,
                58
            }
        );

        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        Assert.Equal(4361, result);
    }
}