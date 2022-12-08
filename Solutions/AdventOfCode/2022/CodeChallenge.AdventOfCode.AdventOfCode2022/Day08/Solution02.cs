namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day08;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 8, 2)]
internal class Solution02 : AdventOfCodeSolution<int[][], int>
{
    public Solution02(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay08InputProvider())
    { }

    protected override int ComputeSolution(int[][] input)
    {
        var treeVisibility = input.Select((trees, i) =>
        {
            return trees.Select((treeHeight, j) =>
            {
                var surroundingTrees = GetSurroundingTrees(input, i, j);
                var visibleTreeCounts = surroundingTrees
                    .Select(treesInDirection =>
                    {
                        var treesLessThanEqualInHeight = treesInDirection.TakeWhile(tree => tree < treeHeight).Count();

                        // If vision was blocked by a tree and not the edge of the grid, include the tree that blocked vision
                        return treesLessThanEqualInHeight != treesInDirection.Length
                            ? treesLessThanEqualInHeight + 1
                            : treesLessThanEqualInHeight;
                    });

                return visibleTreeCounts.Aggregate(1, (acc, x) => acc * x);
            });
        });

        return treeVisibility.Max(x => x.Max());
    }

    private static IEnumerable<int[]> GetSurroundingTrees(int[][] grid, int i, int j)
    {
        // In two cases, we need to reverse the items from the grid so we can iterate from i,j towards the edge
        return new List<int[]>
        {
            grid[i][..j].Reverse().ToArray(),
            grid[i][(j + 1)..].ToArray(),
            grid[..i].Reverse().Select(arr => arr[j]).ToArray(),
            grid[(i + 1)..].Select(arr => arr[j]).ToArray()
        };
    }
}
