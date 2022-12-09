namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day09;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day09.Models;
using CodeChallenge.Core.IO;

internal static class Day09InputProviderBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<MoveInstruction>> BuildDay09InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ParseAs<MoveInstruction>()
            .Build();
    }
}