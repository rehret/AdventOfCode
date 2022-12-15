namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day15;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day15.Models;
using CodeChallenge.Core.IO;

internal static class Day15InputProviderBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<Sensor>> BuildDay15InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ParseAs<Sensor>()
            .Build();
    }
}