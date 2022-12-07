namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day01;

using CodeChallenge.Core.IO;

internal static class Day01InputBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<IEnumerable<int>>> BuildDay01InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadChunks(string.IsNullOrWhiteSpace)
            .ParseAs<int>()
            .Build();
    }
}