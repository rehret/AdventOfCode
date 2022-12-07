namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Tests.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2021.Day02;
using CodeChallenge.AdventOfCode.AdventOfCode2021.Day02.Models;
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
    public async Task ComputeSolutionAsync_GivenSampleInput_ProducesSampleOutput()
    {
        // Arrange
        var input = new SubmarineInstruction[]
        {
            new(SubmarineMovement.Forward, 5),
            new(SubmarineMovement.Down, 5),
            new(SubmarineMovement.Forward, 8),
            new(SubmarineMovement.Up, 3),
            new(SubmarineMovement.Down, 8),
            new(SubmarineMovement.Forward, 2)
        };

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal(900, result);
    }
}