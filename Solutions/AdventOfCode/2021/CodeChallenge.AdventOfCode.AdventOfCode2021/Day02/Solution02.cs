namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2021.Day02.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2021, 2, 2)]
internal class Solution02 : AdventOfCodeSolution<IEnumerable<SubmarineInstruction>, int>
{
    public Solution02(IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<SubmarineInstruction>> inputProvider) : base(inputProvider) { }

    protected override int ComputeSolution(IEnumerable<SubmarineInstruction> instructions)
    {
        var result = instructions.Aggregate(new SubmarinePosition(), ExecuteInstruction);
        return result.X * result.Y;
    }

    private static SubmarinePosition ExecuteInstruction(SubmarinePosition position, SubmarineInstruction instruction) => instruction.Movement switch
    {
        SubmarineMovement.Forward => position with { X = position.X + instruction.Amount, Y = position.Y + position.Aim * instruction.Amount },
        SubmarineMovement.Up      => position with { Aim = position.Aim - instruction.Amount },
        SubmarineMovement.Down    => position with { Aim = position.Aim + instruction.Amount },
        _                         => throw new ArgumentOutOfRangeException()
    };
}