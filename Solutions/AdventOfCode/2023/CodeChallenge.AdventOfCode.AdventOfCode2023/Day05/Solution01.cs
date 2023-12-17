namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day05;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day05.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2023, 5, 1)]
internal class Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
    : AdventOfCodeSolution<Almanac, long>(inputProviderBuilder.BuildDay05InputProvider())
{
    protected override long ComputeSolution(Almanac almanac)
    {
        return almanac.SeedIds
            .Select(seedId => FindLocationForSeed(almanac.Maps, seedId))
            .Min();
    }

    private static long FindLocationForSeed(IEnumerable<Map> maps, long seedId)
    {
        return maps.Aggregate(seedId, (result, map) => map.MapValue(result));
    }
}
