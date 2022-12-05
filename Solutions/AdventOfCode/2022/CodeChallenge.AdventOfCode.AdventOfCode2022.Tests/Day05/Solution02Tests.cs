namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day05;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day05;
using CodeChallenge.AdventOfCode.AdventOfCode2022.Day05.Models;
using CodeChallenge.Core.IO;

public class Solution02Tests
{
    private readonly Solution02 _solution;

    public Solution02Tests()
    {
        var inputProviderMock = new Mock<IInputProvider<AdventOfCodeChallengeSelection, StacksAndInstructions>>();
        _solution = new Solution02(inputProviderMock.Object);
    }

    [Fact]
    public async Task ComputeSolutionAsync_WithSampleInput_ProducesSampleOutput()
    {
        // Arrange
        var stack0 = new Stack<char>();
        stack0.Push('Z');
        stack0.Push('N');

        var stack1 = new Stack<char>();
        stack1.Push('M');
        stack1.Push('C');
        stack1.Push('D');

        var stack2 = new Stack<char>();
        stack2.Push('P');

        var input = new StacksAndInstructions(
            new[] { stack0, stack1, stack2 },
            new[]
            {
                new MoveInstruction(1, 1, 0),
                new MoveInstruction(3, 0, 2),
                new MoveInstruction(2, 1, 0),
                new MoveInstruction(1, 0, 1)
            }
        );

        // Act
        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        // Assert
        Assert.Equal("MCD", result);
    }
}