namespace AdventOfCode2021.Day02;

using AdventOfCode2021.Day02.Models;

[Solution(2021, 2, 2)]
public class Solution02 : AbstractSolution<SubmarineInstruction, int>
{
    public Solution02(IInputProvider<SubmarineInstruction> inputProvider) : base(inputProvider) { }

    public override Task<int> ComputeSolutionAsync(IEnumerable<SubmarineInstruction> instructions)
    {
        var result = instructions.Aggregate(new SubmarinePosition(), ExecuteInstruction);
        return Task.FromResult(result.X * result.Y);
    }

    private static SubmarinePosition ExecuteInstruction(SubmarinePosition position, SubmarineInstruction instruction) => instruction.Movement switch
    {
        SubmarineMovement.Forward => position with { X = position.X + instruction.Amount, Y = position.Y + position.Aim * instruction.Amount },
        SubmarineMovement.Up      => position with { Aim = position.Aim - instruction.Amount },
        SubmarineMovement.Down    => position with { Aim = position.Aim + instruction.Amount },
        _                         => throw new ArgumentOutOfRangeException()
    };
}