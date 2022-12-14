namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day14;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day14.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 14, 1)]
internal class Solution01 : AdventOfCodeSolution<Material[][], int>
{
    private static readonly Coordinate SandSource = new(500, 0);

    public Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay14InputProvider())
    { }

    protected override int ComputeSolution(Material[][] grid)
    {
        var sandFellToAbyss = false;
        var sandUnitCount = 0;

        while (!sandFellToAbyss)
        {
            var sand = SandSource with { };
            var isFalling = true;
            while (isFalling)
            {
                var didFall = Day14Helpers.TryFall(grid, sand, out var newSand);
                if (!didFall)
                {
                    isFalling = false;
                    if (newSand == null)
                    {
                        sandFellToAbyss = true;
                    }
                    else
                    {
                        grid[newSand.Value.Y][newSand.Value.X] = Material.Sand;
                        sandUnitCount++;
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
