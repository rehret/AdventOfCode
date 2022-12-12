namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day12;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day12.Models;
using CodeChallenge.Core.Helpers.Math;
using CodeChallenge.Core.IO;

internal static class Day12InputProviderBuilderExtensions
{
    public enum DijkstraType
    {
        StartIsSource,
        EndIsSource
    }

    public static IInputProvider<AdventOfCodeChallengeSelection, HeightmapWithStartAndEnd> BuildDay12InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder,
        DijkstraType type
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ParseUsing<HeightmapWithStartAndEnd>(lines => Parse(lines, type))
            .Build();
    }

    private static HeightmapWithStartAndEnd Parse(IEnumerable<string> lines, DijkstraType type)
    {
        var start = new Coordinate(0, 0, 0);
        var end = new Coordinate(0, 0, 0);

        var coordinateGrid = lines.Select((line, y) =>
        {
            return line.ToCharArray()
                .Select((@char, x) =>
                {
                    switch (@char)
                    {
                        case 'S':
                            start = new Coordinate(x, y, 0);
                            return start;
                        case 'E':
                            end = new Coordinate(x, y, 'z' - 'a');
                            return end;
                        default:
                            return new Coordinate(x, y, @char - 'a');
                    }
                })
                .ToArray();
        }).ToArray();

        var graph = new Graph<Coordinate>();
        for (var y = 0; y < coordinateGrid.Length; y++)
        {
            for (var x = 0; x < coordinateGrid[y].Length; x++)
            {
                var coordinate = coordinateGrid[y][x];
                graph.AddVertex(coordinate);
                var neighbors = GetNeighbors(coordinateGrid, x, y, type);
                foreach (var neighbor in neighbors)
                {
                    graph.AddEdge(coordinate, neighbor);
                }
            }
        }

        return new HeightmapWithStartAndEnd(graph, start, end);
    }

    private static IEnumerable<Coordinate> GetNeighbors(IReadOnlyList<IReadOnlyList<Coordinate>> coordinates, int x, int y, DijkstraType type)
    {
        Func<Coordinate, Coordinate, bool> comparer = type == DijkstraType.StartIsSource
            ? (coord, other) => other.Height <= coord.Height + 1
            : (coord, other) => other.Height >= coord.Height - 1;

        var neighbors = new List<Coordinate>();
        var coordinate = coordinates[y][x];

        if (x > 0 && comparer(coordinate, coordinates[y][x - 1]))
            neighbors.Add(coordinates[y][x - 1]);
        if (x < coordinates[y].Count - 1 && comparer(coordinate, coordinates[y][x + 1]))
            neighbors.Add(coordinates[y][x + 1]);
        if (y > 0 && comparer(coordinate, coordinates[y - 1][x]))
            neighbors.Add(coordinates[y - 1][x]);
        if (y < coordinates.Count - 1 && comparer(coordinate, coordinates[y + 1][x]))
            neighbors.Add(coordinates[y + 1][x]);

        return neighbors;
    }
}