namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day03;

using CodeChallenge.Core.IO;

internal static class Day03InputBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<string>> BuildDay03InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .AsStrings()
            .Build();
    }
}