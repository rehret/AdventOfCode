namespace AdventOfCode2021.Day02;

using AdventOfCode2021.Day02.Models;

[Solution(2021, 2, 1)]
public class Solution01 : AbstractSolution<SubmarineInstruction, int>
{
    public Solution01(IInputProvider<SubmarineInstruction> inputProvider) : base(inputProvider) { }

    public override Task<int> ComputeSolutionAsync(IEnumerable<SubmarineInstruction> instructions)
    {
        var result = instructions.Aggregate(new SubmarinePosition(), ExecuteInstruction);
        return Task.FromResult(result.X * result.Y);
    }

    private static SubmarinePosition ExecuteInstruction(SubmarinePosition position, SubmarineInstruction instruction) => instruction.Movement switch
    {
        SubmarineMovement.Forward => position with { X = position.X + instruction.Amount },
        SubmarineMovement.Up      => position with { Y = position.Y - instruction.Amount },
        SubmarineMovement.Down    => position with { Y = position.Y + instruction.Amount },
        _                         => throw new ArgumentOutOfRangeException()
    };
}