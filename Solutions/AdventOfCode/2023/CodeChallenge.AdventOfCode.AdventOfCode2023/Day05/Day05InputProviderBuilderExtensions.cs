namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day05;

using System.Text.RegularExpressions;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day05.Models;
using CodeChallenge.Core.Extensions;
using CodeChallenge.Core.IO;

internal static partial class Day05InputProviderBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, Almanac> BuildDay05InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadChunks(line => line.StartsWith("seeds:", StringComparison.OrdinalIgnoreCase) || SectionHeaderRegex.IsMatch(line.Trim()), ChunkWhenFlags.IncludeMatchedItemInNextChunk)
            .ParseUsing((IEnumerable<IEnumerable<string>> chunks) =>
            {
                // Since chunk indicator lines are being included in the "next" chunk (i.e. they should be the first line in each chunk),
                // the first chunk in the result is empty because there's nothing before the first line.
                // Since the first chunk is empty, skip it.
                var chunksArray = chunks.Skip(1).ToArray();

                var seedNumbers = string.Join(string.Empty, chunksArray.First())
                    .Split(':', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Last()
                    .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse);

                var maps = chunksArray.Skip(1)
                    .Select(chunk =>
                    {
                        var mappings = chunk.Skip(1)
                            .Select(line => line.Trim())
                            .Where(line => line != string.Empty)
                            .Select(line =>
                            {
                                var mappingDefinition = line.Split(' ', 3, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                                    .Select(long.Parse)
                                    .ToArray();

                                var destinationStart = mappingDefinition[0];
                                var sourceStart = mappingDefinition[1];
                                var range = mappingDefinition[2];

                                return new Mapping(sourceStart, destinationStart, range);
                            });

                        return new Map(mappings);
                    });

                return new Almanac(seedNumbers, maps);
            })
            .Build();
    }

    private static readonly Regex SectionHeaderRegex = GetSectionHeaderRegex();

    [GeneratedRegex(@"^\w+-to-\w+ map:$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
    private static partial Regex GetSectionHeaderRegex();
}