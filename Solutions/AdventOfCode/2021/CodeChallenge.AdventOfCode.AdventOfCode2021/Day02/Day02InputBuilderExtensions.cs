namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2021.Day02.Models;
using CodeChallenge.Core.IO;

internal static class Day02InputBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<SubmarineInstruction>> BuildDay02InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ParseAs<SubmarineInstruction>()
            .Build();
    }
}