namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day09;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day09;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day09.Models;
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
        var input = new MoveInstruction[]
        {
            new(MoveDirection.Right, 4),
            new(MoveDirection.Up, 4),
            new(MoveDirection.Left, 3),
            new(MoveDirection.Down, 1),
            new(MoveDirection.Right, 4),
            new(MoveDirection.Down, 1),
            new(MoveDirection.Left, 5),
            new(MoveDirection.Right, 2)
        };

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(13, result);
    }
}