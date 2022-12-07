namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day04;

using CodeChallenge.Core.IO;

internal static class Day04InputBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<(Range, Range)>> BuildDay04InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ParseUsing(line =>
            {
                var parts = line.Split(',', 2, StringSplitOptions.TrimEntries);
                return (BuildRange(parts[0]), BuildRange(parts[1]));
            })
            .Build();
    }

    private static Range BuildRange(string rangeString)
    {
        var parts = rangeString.Split('-', 2, StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToArray();

        // Add one to the second value because the input uses an inclusive upper bound and Range uses an exclusive upper bound
        return new Range(parts[0], parts[1] + 1);
    }
}