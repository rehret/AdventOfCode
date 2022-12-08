namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day08;

using CodeChallenge.Core.IO;

internal static class Day08InputProviderBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, int[][]> BuildDay08InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ParseUsing((IEnumerable<string> lines) => lines.Select<string, int[]>(line =>
            {
                var characters = line.ToCharArray().Where(char.IsNumber).Select(x => x.ToString());
                return characters.Select(int.Parse).ToArray();
            }).ToArray())
            .Build();
    }
}