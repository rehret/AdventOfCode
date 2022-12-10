namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day10;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day10.Models;
using CodeChallenge.Core.IO;

internal static class Day10InputProviderBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<Instruction>> BuildDay10InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ParseUsing<Instruction>(line =>
            {
                var parts = line.Split(' ', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                return parts[0].ToLower() switch
                {
                    "noop" => new NoOpInstruction(),
                    "addx" => new AddXInstruction(int.Parse(parts[1])),
                    _      => throw new FormatException($"Unsupported instruction: '{parts[0]}'")
                };
            })
            .Build();
    }
}