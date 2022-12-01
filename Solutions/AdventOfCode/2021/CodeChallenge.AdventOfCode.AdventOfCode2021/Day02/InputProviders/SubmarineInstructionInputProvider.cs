namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Day02.InputProviders;

using CodeChallenge.AdventOfCode.AdventOfCode2021.Day02.Models;
using CodeChallenge.Core.IO;
using CodeChallenge.Core.IO.InputProviders;

internal class SubmarineInstructionInputProvider : AbstractInputProvider<AdventOfCodeChallengeSelection, IEnumerable<SubmarineInstruction>>
{
    public SubmarineInstructionInputProvider(IInputReader<AdventOfCodeChallengeSelection> inputReader)
        : base(inputReader)
    { }

    protected override IEnumerable<SubmarineInstruction> ParseLines(IEnumerable<string> lines)
    {
        return lines.Select(ProcessLine);
    }

    private static SubmarineInstruction ProcessLine(string line)
    {
        var values = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (values.Length > 2)
        {
            throw new Exception($"Invalid number of argument on line: '{line}'");
        }

        return new SubmarineInstruction(GetMovementFromString(values[0]), int.Parse(values[1]));
    }

    private static SubmarineMovement GetMovementFromString(string movement) => movement.ToLower() switch
    {
        "forward" => SubmarineMovement.Forward,
        "up"      => SubmarineMovement.Up,
        "down"    => SubmarineMovement.Down,
        _         => throw new ArgumentOutOfRangeException()
    };
}