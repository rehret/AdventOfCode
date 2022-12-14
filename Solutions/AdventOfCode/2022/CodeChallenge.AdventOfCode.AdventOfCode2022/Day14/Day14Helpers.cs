namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day14;

using System.Diagnostics.CodeAnalysis;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day14.Models;

internal static class Day14Helpers
{
    public static bool TryFall(IReadOnlyList<IReadOnlyList<Material>> grid, Coordinate start, [NotNullWhen(true)] out Coordinate? end)
    {
        if (start.Y >= grid.Count - 1)
        {
            end = null;
            return false;
        }

        if (grid[start.Y + 1][start.X] == Material.Air)
        {
            end = start with { Y = start.Y + 1 };
            return true;
        }

        if (start.X == 0 || start.X >= grid[start.Y].Count - 1)
        {
            end = null;
            return false;
        }

        if (grid[start.Y + 1][start.X - 1] == Material.Air)
        {
            end = new Coordinate(X: start.X - 1, Y: start.Y + 1);
            return true;
        }

        if (grid[start.Y + 1][start.X + 1] == Material.Air)
        {
            end = new Coordinate(X: start.X + 1, Y: start.Y + 1);
            return true;
        }

        end = start;
        return false;
    }
}