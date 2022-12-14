namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day14;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day14;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day14.Models;
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
        // Arrange
        var input = new Material[10][];
        for (var i = 0; i < input.Length; i++)
        {
            input[i] = new Material[503 * 2];
            Array.Fill(input[i], Material.Air);
        }

        input[4][498] = Material.Rock;
        input[5][498] = Material.Rock;
        input[6][498] = Material.Rock;
        input[6][497] = Material.Rock;
        input[6][496] = Material.Rock;

        input[4][503] = Material.Rock;
        input[4][502] = Material.Rock;
        input[5][502] = Material.Rock;
        input[6][502] = Material.Rock;
        input[7][502] = Material.Rock;
        input[8][502] = Material.Rock;
        input[9][502] = Material.Rock;
        input[9][501] = Material.Rock;
        input[9][500] = Material.Rock;
        input[9][499] = Material.Rock;
        input[9][498] = Material.Rock;
        input[9][497] = Material.Rock;
        input[9][496] = Material.Rock;
        input[9][495] = Material.Rock;
        input[9][494] = Material.Rock;

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(24, result);
    }
}