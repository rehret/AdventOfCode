namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day05.InputProviders;

using System.Text.RegularExpressions;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day05.Models;
using CodeChallenge.Core.Extensions;
using CodeChallenge.Core.IO;

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
        var linesArray = lines.ToArray();

        var stackPositions = linesArray
            .Last()
            .ToCharArray()
            .Select((@char, index) => (CharacterPosition: index, StackNumber: @char))
            .Where(x => char.IsNumber(x.StackNumber))
            .ToDictionary(
                x => int.Parse(x.StackNumber.ToString()) - 1,
                x => x.CharacterPosition
            );

        var stacks = new Stack<char>[stackPositions.Keys.Count];
        for (var i = 0; i < stackPositions.Keys.Count; i++)
        {
            stacks[i] = new Stack<char>();
        }

        var stackInputs = linesArray
            .Take(linesArray.Length - 1); // Remove last line because it only has the stack numbers

         foreach (var stackInput in stackInputs.Reverse()) // Reverse so we push bottom items first
         {
             foreach (var (stackNumber, characterPosition) in stackPositions)
             {
                 if (char.IsLetter(stackInput[characterPosition]))
                 {
                     stacks[stackNumber].Push(stackInput[characterPosition]);
                 }
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