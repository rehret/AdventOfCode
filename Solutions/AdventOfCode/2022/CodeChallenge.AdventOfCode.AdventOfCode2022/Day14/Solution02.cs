namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day14;

using System.Diagnostics;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day14.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 14, 2)]
internal class Solution02 : AdventOfCodeSolution<Material[][], int>
{
    private static readonly Coordinate SandSource = new(500, 0);

    public Solution02(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay14InputProvider(2))
    { }

    protected override int ComputeSolution(Material[][] grid)
    {
        var sandStoppedAtSource = false;
        var sandUnitCount = 0;

        while (!sandStoppedAtSource)
        {
            var sand = SandSource with { };
            var isFalling = true;
            while (isFalling)
            {
                var didFall = Day14Helpers.TryFall(grid, sand, out var newSand);
                if (!didFall)
                {
                    if (newSand == null)
                    {
                        throw new UnreachableException("Sand should not be able to fall into the abyss");
                    }

                    isFalling = false;
                    sandUnitCount++;
                    if (newSand == SandSource)
                    {
                        sandStoppedAtSource = true;
                    }
                    else
                    {
                        grid[newSand.Value.Y][newSand.Value.X] = Material.Sand;
                    }
                }
                else
                {
                    sand = newSand!.Value;
                }
            }
        }

        return sandUnitCount;
    }
}
