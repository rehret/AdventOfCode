namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Day02.InputProviders;

using System.Text.RegularExpressions;

using CodeChallenge.AdventOfCode.AdventOfCode2021.Day02.Models;
using CodeChallenge.Core;

internal class SubmarineInstructionInputProvider : AbstractInputProvider<AdventOfCodeChallengeSelection, SubmarineInstruction>
{
    private static readonly Regex WhitespaceRegex = new(@"\s+", RegexOptions.Compiled);

    public SubmarineInstructionInputProvider(IInputReader<AdventOfCodeChallengeSelection> inputReader) : base(inputReader) { }

    protected override SubmarineInstruction ProcessLine(string line)
    {
        var values = WhitespaceRegex.Split(line).Select(value => value.Trim()).ToArray();
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