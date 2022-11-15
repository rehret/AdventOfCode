namespace AdventOfCode2021.Day02;

using AdventOfCode;

using AdventOfCode2021.Day02.Models;

internal class Solution02 : AbstractSolution<SubmarineInstruction>
{
    public Solution02(IInputProcessor<SubmarineInstruction> inputProcessor) : base(inputProcessor) { }

    public override Task<string> ComputeSolutionAsync(IEnumerable<SubmarineInstruction> instructions)
    {
        var result = instructions.Aggregate(new SubmarinePosition(), ExecuteInstruction);
        return Task.FromResult((result.X * result.Y).ToString());
    }

    private static SubmarinePosition ExecuteInstruction(SubmarinePosition position, SubmarineInstruction instruction) => instruction.Movement switch
    {
        SubmarineMovement.Forward => position with { X = position.X + instruction.Amount, Y = position.Y + position.Aim * instruction.Amount },
        SubmarineMovement.Up      => position with { Aim = position.Aim - instruction.Amount },
        SubmarineMovement.Down    => position with { Aim = position.Aim + instruction.Amount },
        _                         => throw new ArgumentOutOfRangeException()
    };
}