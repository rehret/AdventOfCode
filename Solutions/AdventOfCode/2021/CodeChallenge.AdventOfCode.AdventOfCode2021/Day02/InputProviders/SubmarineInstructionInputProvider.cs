namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Day02.InputProviders;

using System.Text.RegularExpressions;

using CodeChallenge.AdventOfCode.AdventOfCode2021.Day02.Models;
using CodeChallenge.Core.IO;

internal class SubmarineInstructionInputProvider : IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<SubmarineInstruction>>
{
    private static readonly Regex WhitespaceRegex = new(@"\s+", RegexOptions.Compiled);

    private readonly IInputReader<AdventOfCodeChallengeSelection> _inputReader;

    public SubmarineInstructionInputProvider(IInputReader<AdventOfCodeChallengeSelection> inputReader)
    {
        _inputReader = inputReader;
    }

    public async Task<IEnumerable<SubmarineInstruction>> GetInputAsync(AdventOfCodeChallengeSelection challengeSelection)
    {
        return (await _inputReader.GetInputAsync(challengeSelection).ConfigureAwait(false))
            .Select(ProcessLine);
    }

    private static SubmarineInstruction ProcessLine(string line)
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