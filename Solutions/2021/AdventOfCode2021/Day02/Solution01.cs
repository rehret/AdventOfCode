namespace AdventOfCode2021.Day02;

using AdventOfCode2021.Day02.Models;

internal class Solution01 : AbstractSolution<SubmarineInstruction>
{
    public Solution01(IInputProcessor<SubmarineInstruction> inputProcessor) : base(inputProcessor) { }

    public override Task<string> ComputeSolutionAsync(IEnumerable<SubmarineInstruction> instructions)
    {
        var result = instructions.Aggregate(new SubmarinePosition(), ExecuteInstruction);
        return Task.FromResult((result.X * result.Y).ToString());
    }

    private static SubmarinePosition ExecuteInstruction(SubmarinePosition position, SubmarineInstruction instruction) => instruction.Movement switch
    {
        SubmarineMovement.Forward => position with { X = position.X + instruction.Amount },
        SubmarineMovement.Up      => position with { Y = position.Y - instruction.Amount },
        SubmarineMovement.Down    => position with { Y = position.Y + instruction.Amount },
        _                         => throw new ArgumentOutOfRangeException()
    };
}