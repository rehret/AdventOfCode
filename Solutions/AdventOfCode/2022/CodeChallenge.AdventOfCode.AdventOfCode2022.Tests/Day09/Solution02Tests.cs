namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day09;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day09;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day09.Models;
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
        var input = new MoveInstruction[]
        {
            new(MoveDirection.Right, 5),
            new(MoveDirection.Up, 8),
            new(MoveDirection.Left, 8),
            new(MoveDirection.Down, 3),
            new(MoveDirection.Right, 17),
            new(MoveDirection.Down, 10),
            new(MoveDirection.Left, 25),
            new(MoveDirection.Up, 20)
        };

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(36, result);
    }
}