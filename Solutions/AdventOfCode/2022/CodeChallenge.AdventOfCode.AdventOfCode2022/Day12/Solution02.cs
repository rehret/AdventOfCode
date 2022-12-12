namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day12;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day12.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.Helpers.Math;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 12, 2)]
internal class Solution02 : AdventOfCodeSolution<HeightmapWithStartAndEnd, int>
{
    public Solution02(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay12InputProvider(Day12InputProviderBuilderExtensions.DijkstraType.EndIsSource))
    { }

    protected override int ComputeSolution(HeightmapWithStartAndEnd input)
    {
        var (heightmap, _, end) = input;
        var shortestPaths = Dijkstra.GetShortestPathGraph(heightmap, end);

        return heightmap.GetVertices()
            .Where(x => x.Height == 0)
            .Select(x => shortestPaths.Distances[x])
            .Min();
    }
}
