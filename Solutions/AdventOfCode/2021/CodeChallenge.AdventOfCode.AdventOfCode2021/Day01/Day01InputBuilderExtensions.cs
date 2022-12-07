namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Day01;

using CodeChallenge.Core.IO;

internal static class Day01InputBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<int>> BuildDay01InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ParseAs<int>()
            .Build();
    }
}