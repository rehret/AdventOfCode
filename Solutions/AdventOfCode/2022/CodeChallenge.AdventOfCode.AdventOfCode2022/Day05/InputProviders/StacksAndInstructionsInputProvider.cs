namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day05.InputProviders;

using System.Text.RegularExpressions;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day05.Models;
using CodeChallenge.Core.Extensions;
using CodeChallenge.Core.IO;
using CodeChallenge.Core.IO.InputProviders;

internal class StacksAndInstructionsInputProvider : IInputProvider<AdventOfCodeChallengeSelection, StacksAndInstructions>
{
    private static readonly Regex MoveInstructionRegex = new(@"^move\s+(?<Count>\d+)\s+from\s+(?<Source>\d+)\s+to\s+(?<Destination>\d+)$", RegexOptions.Compiled);

    private readonly IInputReader<AdventOfCodeChallengeSelection> _inputReader;

    public StacksAndInstructionsInputProvider(IInputReader<AdventOfCodeChallengeSelection> inputReader)
    {
        _inputReader = inputReader;
    }

    public async Task<StacksAndInstructions> GetInputAsync(AdventOfCodeChallengeSelection challengeSelection)
    {
        var input = await _inputReader.GetInputAsync(challengeSelection).ConfigureAwait(false);
        var chunks = input
            .ChunkWhen(string.IsNullOrWhiteSpace)
            .Take(2)
            .ToArray();

        return new StacksAndInstructions(
            ParseStacks(chunks[0]),
            ParseMoveInstructions(chunks[1])
        );
    }

    private static Stack<char>[] ParseStacks(IEnumerable<string> lines)
    {
        const int stackColumnWidth = 4; // Each column is a left square-bracket, a letter, a right square bracket, and a space (ex: '[X] ')

        var linesArray = lines.ToArray();

        var numberOfColumns = linesArray
            .Last()
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .Max();

        var stacks = new Stack<char>[numberOfColumns];
        for (var i = 0; i < numberOfColumns; i++)
        {
            stacks[i] = new Stack<char>();
        }

        var stackInputs = linesArray
            .Take(linesArray.Length - 1) // Remove last line because it only has the stack numbers
            .Reverse()
            .Select(line => line.ToCharArray().Chunk(stackColumnWidth).Select(boxChars => string.Join("", boxChars)))
            .Select(boxes => boxes.Select((box, index) => (index, box[1])).Where(x => char.IsLetter(x.Item2)))
            .Select(x => x.ToArray());

        foreach (var stackInput in stackInputs)
        {
            foreach (var (index, box) in stackInput)
            {
                stacks[index].Push(box);
            }
        }

        return stacks;
    }

    private static IEnumerable<MoveInstruction> ParseMoveInstructions(IEnumerable<string> lines)
    {
        return lines
            .Select(line => MoveInstructionRegex.Match(line))
            .Where(match => match.Success)
            .Select(match => new MoveInstruction(
                int.Parse(match.Groups["Count"].Value),
                int.Parse(match.Groups["Source"].Value) - 1,
                int.Parse(match.Groups["Destination"].Value) - 1
            ));
    }
}