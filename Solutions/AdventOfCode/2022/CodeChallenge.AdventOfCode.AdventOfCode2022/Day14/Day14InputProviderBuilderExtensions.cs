namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day14;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day14.Models;
using CodeChallenge.Core.IO;

internal static class Day14InputProviderBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, Material[][]> BuildDay14InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder,
        int? floorPositionBelowMaxY = null
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ParseUsing((IEnumerable<string> lines) =>
            {
                var rockFormations = lines
                    .Select(line =>
                        line.Split("->", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                            .Select(stringCoord =>
                            {
                                var parts = stringCoord.Split(',',
                                    2,
                                    StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                                if (parts.Length != 2)
                                {
                                    throw new FormatException($"Could not parse {nameof(Coordinate)} from '{stringCoord}'");
                                }

                                return new Coordinate(int.Parse(parts[0]), int.Parse(parts[1]));
                            }))
                    .ToList();

                var maxX = rockFormations.Max(x => x.Max(coord => coord.X));
                var maxY = rockFormations.Max(x => x.Max(coord => coord.Y));

                if (floorPositionBelowMaxY != null)
                {
                    maxY += floorPositionBelowMaxY.Value;
                }

                var grid = new Material[maxY + 1][];
                for (var i = 0; i < grid.Length; i++)
                {
                    // Make grid twice as wide as it needs to be so sand can fall far to the sides
                    grid[i] = new Material[maxX * 2];
                    Array.Fill(grid[i], Material.Air);
                }

                if (floorPositionBelowMaxY != null)
                {
                    Array.Fill(grid[^1], Material.Rock);
                }

                foreach (var rockFormation in rockFormations)
                {
                    var coordinates = rockFormation as Coordinate[] ?? rockFormation.ToArray();
                    var current = coordinates.First();
                    foreach (var next in coordinates.Skip(1))
                    {
                        if (current.X == next.X)
                        {
                            for (var y = current.Y; y != next.Y; y += Math.Sign(next.Y - current.Y))
                            {
                                grid[y][current.X] = Material.Rock;
                            }
                        }
                        else
                        {
                            for (var x = current.X; x != next.X; x += Math.Sign(next.X - current.X))
                            {
                                grid[current.Y][x] = Material.Rock;
                            }
                        }

                        grid[next.Y][next.X] = Material.Rock;
                        current = next;
                    }
                }

                return grid;
            })
            .Build();
    }
}