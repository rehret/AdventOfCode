namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day05;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day05.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2023, 5, 2)]
internal class Solution02(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
    : AdventOfCodeSolution<Almanac, long>(inputProviderBuilder.BuildDay05InputProvider())
{
    protected override long ComputeSolution(Almanac almanac)
    {
        // TODO: This brute-force solution doesn't find the solution with the real input within
        // a reasonable about of time. Rather than operate on each input Seed ID, it would be better
        // to perform the calculations on entire ranges of Seed IDs, since the final result for seed n+1
        // is usually f(n) + 1. The ranges will need to be split where there's a discontinuity
        // (i.e. where f(n+1) != f(n) + 1).
        return almanac.SeedIds
            .Select((seedId, index) => (IndexModTwo: index / 2, SeedId: seedId))
            .GroupBy(tuple => tuple.IndexModTwo, tuple => tuple.SeedId)
            .SelectMany(grouping => GetRange(grouping.First(), grouping.Last()))
            .Select(seedId => FindLocationForSeed(almanac.Maps, seedId))
            .Min();
    }

    private static long FindLocationForSeed(IEnumerable<Map> maps, long seedId)
    {
        return maps.Aggregate(seedId, (result, map) => map.MapValue(result));
    }

    private static IEnumerable<long> GetRange(long start, long range)
    {
        for (var i = 0L; i < range; i++)
        {
            yield return start + i;
        }
    }
}
