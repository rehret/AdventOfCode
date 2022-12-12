namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day12;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day12.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.Helpers.Math;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 12, 1)]
internal class Solution01 : AdventOfCodeSolution<HeightmapWithStartAndEnd, int>
{
    public Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay12InputProvider(Day12InputProviderBuilderExtensions.DijkstraType.StartIsSource))
    { }

    protected override int ComputeSolution(HeightmapWithStartAndEnd input)
    {
        var (heightmap, start, end) = input;
        var route = Dijkstra.GetShortestPath(heightmap, start, end);

        // The route contains the starting position, so `.Count()` is the number of positions, while `.Count() - 1`
        // is the number of steps, which is what we want.
        return route.Count() - 1;
    }
}
