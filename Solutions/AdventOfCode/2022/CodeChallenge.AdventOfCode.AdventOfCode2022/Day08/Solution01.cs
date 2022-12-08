namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day08;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 8, 1)]
internal class Solution01 : AdventOfCodeSolution<int[][], int>
{
    public Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay08InputProvider())
    { }

    protected override int ComputeSolution(int[][] input)
    {
        var visibleTreeCount = 0;

        for (var i = 1; i < input.Length - 1; i++)
        {
            for (var j = 1; j < input[i].Length - 1; j++)
            {
                var treeHeight = input[i][j];
                var surroundingTrees = GetSurroundingTrees(input, i, j);
                if (surroundingTrees.Any(trees => trees.All(tree => tree < treeHeight)))
                {
                    visibleTreeCount++;
                }
            }
        }

        return visibleTreeCount
            + input.Length * 2
            + (input[0].Length - 2) * 2;
    }

    private static IEnumerable<int[]> GetSurroundingTrees(int[][] grid, int i, int j)
    {
        return new List<int[]>
        {
            grid[i][..j],
            grid[i][(j + 1)..],
            grid[..i].Select(arr => arr[j]).ToArray(),
            grid[(i + 1)..].Select(arr => arr[j]).ToArray()
        };
    }
}
