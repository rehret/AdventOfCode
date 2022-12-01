namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2021.Day02.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2021, 2, 1)]
internal class Solution01 : AdventOfCodeSolution<IEnumerable<SubmarineInstruction>, int>
{
    public Solution01(IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<SubmarineInstruction>> inputProvider) : base(inputProvider) { }

    protected override int ComputeSolution(IEnumerable<SubmarineInstruction> instructions)
    {
        var result = instructions.Aggregate(new SubmarinePosition(), ExecuteInstruction);
        return result.X * result.Y;
    }

    private static SubmarinePosition ExecuteInstruction(SubmarinePosition position, SubmarineInstruction instruction) => instruction.Movement switch
    {
        SubmarineMovement.Forward => position with { X = position.X + instruction.Amount },
        SubmarineMovement.Up      => position with { Y = position.Y - instruction.Amount },
        SubmarineMovement.Down    => position with { Y = position.Y + instruction.Amount },
        _                         => throw new ArgumentOutOfRangeException()
    };
}