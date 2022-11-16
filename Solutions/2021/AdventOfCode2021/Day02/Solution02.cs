namespace AdventOfCode2021.Day02;

using AdventOfCode2021.Day02.Models;

using Microsoft.Extensions.Logging;

internal class Solution02 : AbstractSolution<SubmarineInstruction, int>
{
    public Solution02(IInputReader inputReader, IInputProcessor<SubmarineInstruction> inputProcessor, ILoggerFactory loggerFactory)
        : base(inputReader, inputProcessor, loggerFactory)
    { }

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