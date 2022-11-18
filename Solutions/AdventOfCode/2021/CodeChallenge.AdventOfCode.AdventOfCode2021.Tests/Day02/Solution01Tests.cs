namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Tests.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2021.Day02;
using CodeChallenge.AdventOfCode.AdventOfCode2021.Day02.Models;
using CodeChallenge.Core;

public class Solution01Tests
{
    private readonly Solution01 _solution;

    public Solution01Tests()
    {
        _solution = new Solution01(new Mock<IInputProvider<AdventOfCodeChallengeSelection, SubmarineInstruction>>().Object);
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
        Assert.Equal(150, result);
    }
}