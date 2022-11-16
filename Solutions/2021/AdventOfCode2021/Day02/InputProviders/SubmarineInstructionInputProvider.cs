namespace AdventOfCode2021.Day02.InputProviders;

using System.Text.RegularExpressions;

using AdventOfCode2021.Day02.Models;

internal class SubmarineInstructionInputProvider : InputProvider<SubmarineInstruction>
{
    private static readonly Regex WhitespaceRegex = new(@"\s+", RegexOptions.Compiled);

    public SubmarineInstructionInputProvider(IInputReader inputReader) : base(inputReader) { }

    protected override SubmarineInstruction ProcessLine(string line)
    {
        var values = WhitespaceRegex.Split(line.Trim()).Select(value => value.Trim()).ToArray();
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